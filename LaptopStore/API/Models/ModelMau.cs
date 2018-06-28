using System;
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
}
