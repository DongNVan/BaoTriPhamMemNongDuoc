using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtTenDangNhap.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Hãy nhập tên đăng nhập và mật khẩu");
            }
            else
            {
                NhanVienController ctrNV = new NhanVienController();
                if(ctrNV.XacNhanDangNhap(txtTenDangNhap.Text, txtMatKhau.Text) == true)
                {
                    frmMain fMain = new frmMain();
                    fMain.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập và mật khẩu");
                }
            }
        }
    }
}
