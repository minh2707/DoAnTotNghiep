﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

    public class ModelDangKyMau
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật Khẩu phải ít nhất {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mat Khau")]
        public string MatKhau { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhap Lai Mat Khau")]
        [Compare("MatKhau", ErrorMessage = "Mat Khau Khong Hop Le")]
        public string NhapLaiMatKhau { get; set; }
        [Display(Name = "Ho")]
        public string Ho { get; set; }
        [Display(Name = "Ten")]
        public string Ten { get; set; }

    }

    public class ModelDangNhapMau
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật Khẩu phải ít nhất {2}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string MatKhau { get; set; }
    }

    public class SanPhamMau
    {
        public SanPhamMau()
        {
            this.TrangSo = 0;
        }

        public int IdLoai { get; set; }
        public int GiaTu { get; set; }
        public int GiaDen { get; set; }
        public float GiamGia { get; set; }
        public int SoSanPhamTrongMotTrang { get; set; }
        public int TrangSo { get; set; }

    }

    public class ChiTietDonHangMau
    {
        [Display(Name = "Id San Pham")]
        [Required(ErrorMessage = "Phai Co Id San Pham")]
        public int IdSanPham { get; set; }

        [Display(Name = "Id Don Hang")]
        [Required(ErrorMessage = "Phai Co Id Don Hang")]
        public int IdDonHang { get; set; }

        [Required(ErrorMessage = "Giam Gia Bat Buot")]
        public double GiamGia { get; set; }

        [Required(ErrorMessage = "Phai Co So Luong San Pham")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Phai co don gia")]
        public double DonGia { get; set; }
    }
}
