using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API.Controllers
{
    [Produces("application/json")]
    [EnableCors("MyPolicyA")]
    public class CategoriesController : Controller
    {
        private readonly LapTopStoreContext ketnoidatabase;

        public CategoriesController(LapTopStoreContext ketnoi)
        {
            ketnoidatabase = ketnoi;
        }

        [AllowAnonymous]
        [Route("api/LayHetLoaiSanPham")]
        [HttpGet]
        public IEnumerable<LoaiSanPham> LayHetLoaiSanPham()
        {
            return ketnoidatabase.LoaiSanPham;
        }

        [AllowAnonymous]
        [Route("api/LayMotLoaiSanPham/{id}")]
        [HttpGet]
        public async Task<IActionResult> LayMotLoaiSanPham(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loaisanpham = await ketnoidatabase.LoaiSanPham.SingleOrDefaultAsync(m => m.Id == id);

            if (loaisanpham == null)
            {
                return NotFound();
            }

            return Ok(loaisanpham);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        [Route("api/CapNhatLoaiSanPham")]
        public JsonResult CapNhatLoaiSanPham([FromBody]LoaiSanPham loai)
        {
            var cate = ketnoidatabase.LoaiSanPham.Find(loai.Id);
            if (cate == null)
            {
                throw new Exception("Khong the cap nhat");
            }
            else
            {
                cate.Ten = loai.Ten;
                cate.MoTa = loai.MoTa;
                ketnoidatabase.Entry(cate).State = EntityState.Modified;
                ketnoidatabase.SaveChanges();
            }
            return Json(true);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        [Route("api/TaoLoaiSanPham")]
        public JsonResult TaoLoaiSanPham( [FromBody]LoaiSanPham loai)
        {
            var data = ketnoidatabase.LoaiSanPham.Find(loai.Id);
            if (data!=null)
            {
                throw new Exception("Khong Ton Tai Loai San Pham");
            }
            ketnoidatabase.LoaiSanPham.Add(loai);
            ketnoidatabase.SaveChanges();
            return Json(true);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        [HttpDelete("api/XoaLoaiSanPham/{id}")]
        public async Task<IActionResult> XoaLoai([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loai = await ketnoidatabase.LoaiSanPham.SingleOrDefaultAsync(m => m.Id == id);
            if (loai == null)
            {
                return NotFound();
            }

            ketnoidatabase.LoaiSanPham.Remove(loai);
            await ketnoidatabase.SaveChangesAsync();

            return Ok(loai);
        }

        private bool KiemTraTonTaiLoai(int id)
        {
            return ketnoidatabase.LoaiSanPham.Any(e => e.Id == id);
        }
    }
}