using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class ChiTietDonHang
    {
        public int Id { get; set; }
        public int IddonHang { get; set; }
        public int IdsanPham { get; set; }
        public double GiamGia { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }

        public DonHang IddonHangNavigation { get; set; }
        public SanPham IdsanPhamNavigation { get; set; }
    }
}
