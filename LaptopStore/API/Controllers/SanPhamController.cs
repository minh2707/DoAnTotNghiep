using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SanPhamController : Controller
    {
        private readonly LapTopStoreContext ketnoidatabase;

        public SanPhamController(LapTopStoreContext ketnoi)
        {
            ketnoidatabase = ketnoi;
        }

        [HttpGet]
        [Route("api/LayVaTimSanPham")]
        public async Task<List<SanPham>> LayVaTimSanPham([FromQuery] SanPhamMau sp)
        {
            var tatcasanpham = ketnoidatabase.SanPham.AsQueryable();
            var tongsosanphamdangco = ketnoidatabase.SanPham.Count();

            if (sp.IdLoai != 0)
            {
                tatcasanpham = tatcasanpham.Where(m => m.Idloai == sp.IdLoai);
            }

            if (sp.GiaDen || sp.GiaTu) {
                if (sp.GiaDen != 0)
                {
                    tatcasanpham = tatcasanpham.Where(m => m.Gia >= sp.GiaTu && m.Gia <= sp.GiaDen);
                }
                else
                {
                    tatcasanpham = tatcasanpham.Where(m => m.Gia >= sp.GiaTu);
                }
            }

            if (sp.GiamGia != 0)
            {
                tatcasanpham = tatcasanpham.Where(m => m.GiamGia == sp.GiamGia);
            }

            if (sp.TrangSo != 0)
            {
                tatcasanpham = tatcasanpham.Skip(sp.TrangSo * sp.SoSanPhamTrongMotTrang).Take(sp.TrangSo);
            }

            return await tatcasanpham.ToListAsync();
        }
        [HttpGet]
        [Route("api/DemTatCaSanPham")]
        public async Task<int> DemTatCaSanPham()
        {
            return await ketnoidatabase.SanPham.CountAsync();
        }

        [HttpGet]
        [Route("api/SanPhamMoiNhat")]
        public IEnumerable<SanPham> LaySanPhamMoiNhat()
        {
            return ketnoidatabase.SanPham.OrderByDescending(p => p.Id).Take(5);
        }

        [HttpGet]
        [Route("api/LayMotSanPham/{id}")]
        public async Task<IActionResult> LayMotSanPham([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sanpham = await ketnoidatabase.SanPham.SingleOrDefaultAsync(m => m.Id == id);

            if (sanpham == null)
            {
                return NotFound();
            }

            return Ok(sanpham);
        }

        [HttpPut]
        [Route("api/CapNhatSanPham/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<IActionResult> CapNhatSanPham([FromRoute] int id, [FromBody] SanPham product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            ketnoidatabase.Entry(product).State = EntityState.Modified;

            try
            {
                await ketnoidatabase.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KiemTraSanPhamTonTai(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("api/TaoSanPham")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<JsonResult> TaoSanPham([FromBody] SanPham sanpham)
        {
            var prodt = ketnoidatabase.SanPham.Find(sanpham.Id);
            if (prodt != null)
            {
                throw new Exception("San Pham Khong Ton Tai");
            }
            await ketnoidatabase.SanPham.AddAsync(sanpham);
            ketnoidatabase.SaveChanges();
            return Json(true);
        }


        // DELETE: api/Products/5
        [HttpDelete("api/XoaSanPham/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<IActionResult> XoaSanPham([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sanpham = await ketnoidatabase.SanPham.SingleOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            ketnoidatabase.SanPham.Remove(sanpham);
            await ketnoidatabase.SaveChangesAsync();

            return Ok(sanpham);
        }

        private bool KiemTraSanPhamTonTai(int id)
        {
            return ketnoidatabase.SanPham.Any(e => e.Id == id);
        }
        [Route("api/LayGiaCaoNhat")]
        public async Task<double> LayGiaCaoNhat()
        {
            return await ketnoidatabase.SanPham.MaxAsync(p => p.Gia);
        }

        [Route("api/LayGiaThapNhat")]
        public async Task<double> LayGiaThapNhat()
        {
            return await ketnoidatabase.SanPham.MinAsync(p => p.Gia);
        }

        [HttpGet]
        [Route("api/TimKiemSanPhamTheoTen")]
        public JsonResult TimKiemSanPhamTheoTen([FromQuery]string tensanpham)
        {
            if (!string.IsNullOrEmpty(tensanpham))
            {
                var sp = ketnoidatabase.SanPham.Where(p => p.Ten.Contains(tensanpham));
                return Json(sp);
            }
            return Json(new List<SanPham>());
        }
    }
}