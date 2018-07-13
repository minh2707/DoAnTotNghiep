using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    public class TaiKhoanController : Controller
    {
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
    }
}