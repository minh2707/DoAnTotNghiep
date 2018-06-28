using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHang = new HashSet<DonHang>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public bool TrangThai { get; set; }

        public ICollection<DonHang> DonHang { get; set; }
    }
}
