using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

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
