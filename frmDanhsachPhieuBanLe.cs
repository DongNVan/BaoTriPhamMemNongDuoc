using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmDanhsachPhieuBanLe : Form
    {
        private readonly string idNhanVien; // Refactored: Marked readonly
        private PhieuBanController ctrl;
        private KhachHangController ctrlKH;
        private frmBanLe BanLe;

        private const string ConfirmDeleteMessage = "Bạn có chắc chắn xóa không?"; // Refactored: Removed magic string
        private const string ConfirmDeleteTitle = "Phieu Ban Le"; // Refactored: Removed magic string

        public frmDanhsachPhieuBanLe()
        {
            InitializeComponent();
            InitializeControllers(); // Refactored: Extracted initialization logic
        }

        public frmDanhsachPhieuBanLe(string idNhanVien) : this()
        {
            this.idNhanVien = idNhanVien;
        }

        private void InitializeControllers() // Refactored: New method for initialization
        {
            ctrl = new PhieuBanController();
            ctrlKH = new KhachHangController();
            BanLe = null;
        }

        private void frmDanhsachPhieuNhap_Load(object sender, EventArgs e)
        {
            ctrlKH.HienthiKhachHangDataGridviewComboBox(colKhachhang);
            ctrl.HienthiPhieuBanLe(bindingNavigator, dataGridView);
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            OpenOrActivateBanLe(); // Refactored: Extracted logic
        }

        private void OpenOrActivateBanLe() // Refactored: Extracted method
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmBanLe(ctrl, idNhanVien);
                BanLe.Show();
            }
            else
            {
                BanLe.Activate();
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            OpenOrActivateBanLeForNew(); // Refactored: Extracted logic
        }

        private void OpenOrActivateBanLeForNew() // Refactored: Extracted method
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmBanLe(idNhanVien);
                BanLe.Show();
            }
            else
            {
                BanLe.Activate();
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (ConfirmDelete() == DialogResult.No) // Refactored: Extracted confirmation dialog
            {
                e.Cancel = true;
                return;
            }

            DataRowView view = (DataRowView)bindingNavigator.BindingSource.Current;
            UpdateProductQuantitiesOnDelete(view["ID"].ToString()); // Refactored: Extracted logic
        }

        private DialogResult ConfirmDelete() // Refactored: Extracted method
        {
            return MessageBox.Show(ConfirmDeleteMessage, ConfirmDeleteTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void UpdateProductQuantitiesOnDelete(string phieuId) // Refactored: Extracted method
        {
            var ctrl = new ChiTietPhieuBanController();
            var details = ctrl.ChiTietPhieuBan(phieuId);

            foreach (var detail in details)
            {
                CuahangNongduoc.DataLayer.MaSanPhanFactory.CapNhatSoLuong(detail.MaSanPham.Id, detail.SoLuong);
            }

            ctrl.Save();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DataRowView view = (DataRowView)bindingNavigator.BindingSource.Current;
            if (view == null) return;

            if (ConfirmDelete() == DialogResult.Yes)
            {
                UpdateProductQuantitiesOnDelete(view["ID"].ToString());
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void toolPrint_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PrintPhieuBan(row["ID"].ToString()); // Refactored: Extracted logic
            }
        }

        private void PrintPhieuBan(string maPhieu) // Refactored: Extracted method
        {
            var ph = ctrl.LayPhieuBan(maPhieu);
            var phieuBan = new frmInPhieuBan(ph);
            phieuBan.Show();
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            ShowSearchForm(); // Refactored: Extracted logic
        }

        private void ShowSearchForm() // Refactored: Extracted method
        {
            var searchForm = new frmTimPhieuBanLe(false);
            Point location = PointToScreen(toolTimKiem.Bounds.Location);
            location.Offset(toolTimKiem.Width, toolTimKiem.Height);
            searchForm.Location = location;

            searchForm.ShowDialog();
            if (searchForm.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuBan(searchForm.cmbNCC.SelectedValue.ToString(), searchForm.dtNgayNhap.Value.Date);
            }
        }
    }
}
