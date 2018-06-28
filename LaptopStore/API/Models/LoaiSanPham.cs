using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class LoaiSanPham
    {
        public LoaiSanPham()
        {
            SanPham = new HashSet<SanPham>();
        }

        public int Id { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }

        public ICollection<SanPham> SanPham { get; set; }
    }
}
