using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmPhieuChi : Form
    {
        LyDoChiController ctrlLyDo = new LyDoChiController();
        PhieuChiController ctrl = new PhieuChiController();

        // Hằng số để thay thế cho các giá trị ma thuật
        private const string DeleteConfirmationMessage = "Bạn chắc chắn xóa phiếu chi này không?";
        private const string DeleteConfirmationTitle = "Phieu Chi";
        private const DialogResult DeleteConfirmationYes = DialogResult.Yes;

        // Constructor
        public frmPhieuChi()
        {
            InitializeComponent();
        }

        // Đổi tên phương thức để dễ hiểu hơn và tách các phần khác nhau ra
        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            InitializeDataGridView();
        }

        // Phương thức để khởi tạo các ComboBox
        private void InitializeComboBoxes()
        {
            ctrlLyDo.HienthiAutoComboBox(cmbLyDoChi);
            ctrlLyDo.HienthiDataGridviewComboBox(colLyDoChi);
        }

        // Phương thức để khởi tạo DataGridView
        private void InitializeDataGridView()
        {
            ctrl.HienthiPhieuChi(bindingNavigator, dataGridView, cmbLyDoChi, txtMaPhieu, dtNgayChi, numTongTien, txtGhiChu);
        }

        // Sử dụng Guard Clause thay vì điều kiện lồng nhau để đơn giản hóa logic
        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(DeleteConfirmationMessage, DeleteConfirmationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DeleteConfirmationYes)
            {
                return;
            }

            bindingNavigator.BindingSource.RemoveCurrent();
            ctrl.Save();
        }

        // Sử dụng Extract Method để tách logic xác nhận xóa phiếu chi
        private bool ConfirmDeletion()
        {
            return MessageBox.Show(DeleteConfirmationMessage, DeleteConfirmationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DeleteConfirmationYes;
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!ConfirmDeletion())
            {
                e.Cancel = true;
            }
        }

        // Sử dụng phương thức trực tiếp thay vì biến tạm thời
        private void toolAdd_Click(object sender, EventArgs e)
        {
            ThamSo.PhieuChi = ThamSo.PhieuChi + 1;

            DataRow row = ctrl.NewRow();
            row["ID"] = ThamSo.PhieuChi;
            row["NGAY_CHI"] = dtNgayChi.Value.Date;
            row["TONG_TIEN"] = numTongTien.Value;
            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();
        }

        // Đơn giản hóa biểu thức điều kiện
        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        // Phương thức in phiếu chi
        private void toolIn_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PhieuChiController ctrlChi = new PhieuChiController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuChi ph = ctrlChi.LayPhieuChi(ma_phieu);
                frmInPhieuChi InPhieu = new frmInPhieuChi(ph);
                InPhieu.Show();
            }
        }

        // Mở form LyDoChi khi thêm lý do chi mới
        private void btnThemLyDoChi_Click(object sender, EventArgs e)
        {
            frmLyDoChi Chi = new frmLyDoChi();
            Chi.ShowDialog();
            ctrlLyDo.HienthiAutoComboBox(cmbLyDoChi);
        }

        // Tìm kiếm phiếu chi với bộ lọc lý do chi và ngày chi
        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            frmTimPhieuChi Tim = new frmTimPhieuChi();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            Tim.Location = p;
            Tim.ShowDialog();
            if (Tim.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuChi(bindingNavigator, dataGridView, cmbLyDoChi, txtMaPhieu, dtNgayChi, numTongTien, txtGhiChu, Convert.ToInt32(Tim.cmbLyDo.SelectedValue), dtNgayChi.Value.Date);
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Focus();
            bindingNavigator.BindingSource.MoveNext();
            ctrl.Save();
        }
    }
}
