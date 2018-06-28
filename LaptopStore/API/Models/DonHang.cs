using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDonHang = new HashSet<ChiTietDonHang>();
        }

        public int Id { get; set; }
        public string IdkhachHang { get; set; }
        public string DiaChi { get; set; }
        public double SoLuong { get; set; }
        public DateTime NgayGiao { get; set; }
        public DateTime NgayDat { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }

        public KhachHang IdkhachHangNavigation { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
    }
}
