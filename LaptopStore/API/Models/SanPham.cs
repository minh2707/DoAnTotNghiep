using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietDonHang = new HashSet<ChiTietDonHang>();
        }

        public int Id { get; set; }
        public int Idloai { get; set; }
        public string Ten { get; set; }
        public double Gia { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }
        public int SoLuong { get; set; }
        public double GiamGia { get; set; }
        public bool TrangThai { get; set; }

        public LoaiSanPham IdloaiNavigation { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
    }
}
