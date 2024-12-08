using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmSoLuongTon : Form
    {
        public frmSoLuongTon()
        {
            InitializeComponent();
        }

        private void frmSoLuongTon_Load(object sender, EventArgs e)
        {
            LoadSoLuongTon();
        }

        // Tách logic lấy dữ liệu ra thành phương thức riêng để cải thiện khả năng tái sử dụng và giảm độ phức tạp của sự kiện Load
        private void LoadSoLuongTon()
        {
            try
            {
                var data = GetSoLuongTonData();
                BindDataToReportViewer(data);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        // Thêm phương thức riêng để lấy dữ liệu
        private IList<CuahangNongduoc.BusinessObject.SoLuongTon> GetSoLuongTonData()
        {
            return CuahangNongduoc.Controller.SanPhamController.LaySoLuongTon();
        }

        // Tách phần bind dữ liệu vào ReportViewer thành phương thức riêng
        private void BindDataToReportViewer(IList<CuahangNongduoc.BusinessObject.SoLuongTon> data)
        {
            this.SoLuongTonBindingSource.DataSource = data;
            this.reportViewer.RefreshReport();
        }

        // Thêm phương thức xử lý lỗi để tăng tính rõ ràng và tránh trùng lặp
        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}