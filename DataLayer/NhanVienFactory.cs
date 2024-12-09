using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.DataLayer
{
    public class NhanVienFactory
    {
        DataService m_Ds = new DataService();

      

        public DataTable LayNhanVien(String idNhanVien)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHAN_VIEN WHERE ID_NHAN_VIEN = @id");
            cmd.Parameters.Add("id", OleDbType.VarChar , 50).Value = idNhanVien;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable LayNhanVien(String tenDangNhap, String matKhau)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHAN_VIEN WHERE TEN_DANG_NHAP = @tenDN and MAT_KHAU = @mK");
            cmd.Parameters.Add("tenDN", OleDbType.VarChar, 50).Value = tenDangNhap;
            cmd.Parameters.Add("mK", OleDbType.VarChar, 50).Value = matKhau;
            m_Ds.Load(cmd);
            return m_Ds;
        }
        public String LayIDNhanVien(String tenDangNhap, String matKhau)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHAN_VIEN WHERE TEN_DANG_NHAP = @tenDN and MAT_KHAU = @mK");
            cmd.Parameters.Add("tenDN", OleDbType.VarChar, 50).Value = tenDangNhap;
            cmd.Parameters.Add("mK", OleDbType.VarChar, 50).Value = matKhau;
            m_Ds.Load(cmd);
            DataRow row = m_Ds.Rows[0];
            return row["ID_NHAN_VIEN"].ToString();
        }

        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }
        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
