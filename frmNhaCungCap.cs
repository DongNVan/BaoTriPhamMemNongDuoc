using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmNhaCungCap : Form
    {
        CuahangNongduoc.Controller.NhaCungCapController ctrl = new CuahangNongduoc.Controller.NhaCungCapController();

        // Refactored: Declare constants for search text and placeholder color
        private const string SearchByNameText = "Tìm theo Nhà cung cấp"; // Refactored
        private const string SearchByAddressText = "Tìm theo Địa chỉ"; // Refactored
        private const int PlaceholderTextColor = 224; // Refactored

        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Nha Cung Cap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (ConfirmDelete()) // Refactored: Extracted method for delete confirmation
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        // Refactored: Extracted method to confirm deletion
        private bool ConfirmDelete() // Refactored
        {
            return MessageBox.Show("Bạn có chắc chắn xóa không?", "Nha Cung Cap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            long maso = GetNextMaso(); // Refactored: Used method to get next maso
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.AddNew();
            row["ID"] = maso;
        }

        // Refactored: Extracted method to get next maso
        private long GetNextMaso() // Refactored
        {
            long maso = ThamSo.NhaCungCap;
            ThamSo.NhaCungCap = maso + 1;
            return maso;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            ctrl.Save();
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolTimNhaCungCap_Enter(object sender, EventArgs e)
        {
            toolTimNhaCungCap.Text = "";
            toolTimNhaCungCap.ForeColor = Color.Black;
        }

        private void toolTimNhaCungCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SearchNhaCungCap(); // Refactored: Moved search logic to a separate method
            }
        }

        // Refactored: Extracted search logic to a separate method
        private void SearchNhaCungCap() // Refactored
        {
            var searchText = toolTimNhaCungCap.Text;
            if (toolTimHoTen.Checked)
            {
                ctrl.TimHoTen(searchText);
            }
            else if (toolTimDiaChi.Checked)
            {
                ctrl.TimDiaChi(searchText);
            }
        }

        private void toolTimNhaCungCap_Leave(object sender, EventArgs e)
        {
            toolTimNhaCungCap.Text = toolTimHoTen.Checked ? SearchByNameText : SearchByAddressText; // Refactored: Using constants
            toolTimNhaCungCap.ForeColor = Color.FromArgb(PlaceholderTextColor, PlaceholderTextColor, PlaceholderTextColor); // Refactored: Using constant for color
        }

        private void toolTimHoTen_Click(object sender, EventArgs e)
        {
            ToggleSearchByName(); // Refactored: Extracted search toggle logic to a separate method
        }

        // Refactored: Extracted method to toggle search by name
        private void ToggleSearchByName() // Refactored
        {
            toolTimDiaChi.Checked = false;
            toolTimHoTen.Checked = true;
            toolTimNhaCungCap.Text = SearchByNameText;
            bindingNavigator.Focus();
        }

        private void toolTimDiaChi_Click(object sender, EventArgs e)
        {
            ToggleSearchByAddress(); // Refactored: Extracted search toggle logic to a separate method
        }

        // Refactored: Extracted method to toggle search by address
        private void ToggleSearchByAddress() // Refactored
        {
            toolTimHoTen.Checked = false;
            toolTimDiaChi.Checked = true;
            toolTimNhaCungCap.Text = SearchByAddressText;
            bindingNavigator.Focus();
        }
    }
}