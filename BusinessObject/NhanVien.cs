using System;
using System.Collections.Generic;
using System.Text;

namespace CuahangNongduoc.BusinessObject
{
    public class NhanVien
    {
        private String m_Id;

        public String Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private String m_TenDangNhap;

        public String TenDangNhap
        {
            get { return m_TenDangNhap; }
            set { m_TenDangNhap = value; }
        }

        private String m_MatKhau;
        public String MatKhau
        {
            get { return m_MatKhau; }
            set { m_MatKhau = value; }
        }

        private String m_TenNV;
        public String TenNV
        {
            get { return m_TenNV; }
            set { m_TenNV = value; }
        }
    }
}
