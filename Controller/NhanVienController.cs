using System;
using System.Collections.Generic;
using System.Text;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Windows.Forms;
using System.Data;

namespace CuahangNongduoc.Controller
{

    public class NhanVienController
    {
        NhanVienFactory factory = new NhanVienFactory();



        public void LayNhanVien(DataGridView dgv, String idNhanVien)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = factory.LayNhanVien(idNhanVien);
            dgv.DataSource = bs;
        }
        public bool XacNhanDangNhap(String tenNV, String matKhau)
        {
            DataTable dt = factory.LayNhanVien(tenNV, matKhau);
            if (dt.Rows.Count > 0)
                return true;
            else return false;
        }
        public DataRow NewRow()
        {
            return factory.NewRow();
        }
        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public void Save()
        {
            factory.Save();
        }


        

    }
}
