using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ChiTietDonHangController : Controller
    {
        private readonly LapTopStoreContext ketnoidatabase;

        public ChiTietDonHangController(LapTopStoreContext ketnoi)
        {
            ketnoidatabase = ketnoi;
        }

        [HttpGet]
        [Route("LayHetTatCaChiTietDonHang")]
        public IEnumerable<ChiTietDonHang> LayHetTatCaChiTietDonHang()
        {
            return ketnoidatabase.ChiTietDonHang;
        }

        [HttpGet("LayChiTietDonHang/{id}")]
        public async Task<IActionResult> LayChiTietDonHang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chitietdonhang = await ketnoidatabase.ChiTietDonHang.SingleOrDefaultAsync(m => m.Id == id);

            if (chitietdonhang == null)
            {
                return NotFound();
            }

            return Ok(chitietdonhang);
        }

        [HttpPut("CapNhatChiTietDonHang/{id}")]
        public async Task<IActionResult> CapNhatChiTietDonHang([FromRoute] int id, [FromBody] ChiTietDonHang chitietdonhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chitietdonhang.Id)
            {
                return BadRequest();
            }

            ketnoidatabase.Entry(chitietdonhang).State = EntityState.Modified;

            try
            {
                await ketnoidatabase.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KiemTraSuTonTaiCuaChiTietDonHang(id))
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
        [Route("TaoChiTietDonHang")]
        public async Task<IActionResult> TaoChiTietDonHang([FromBody] ChiTietDonHangMau chitietdonhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ChiTietDonHang chitietdonhangdetao = new ChiTietDonHang()
            {
                IddonHang = chitietdonhang.Id,
                GiamGia = chitietdonhang.GiamGia,
                IdsanPham = chitietdonhang.IdSanPham,
                SoLuong = chitietdonhang.SoLuong,
                DonGia = chitietdonhang.DonGia,

            };

            ketnoidatabase.ChiTietDonHang.Add(chitietdonhangdetao);
            await ketnoidatabase.SaveChangesAsync();

            return CreatedAtAction("LayChiTietDonHang", new { id = chitietdonhang.Id }, chitietdonhangdetao);
        }
        [HttpDelete]
        [Route("XoaChiTietDonHangBangIdDonHang/{iddonhang}")]
        public async Task<IActionResult> XoaChiTietDonHangBangIdDonHang([FromRoute] int iddonhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chitietdh = await ketnoidatabase.ChiTietDonHang.Where(m => m.IddonHang == iddonhang).ToListAsync();
            if (chitietdh == null)
            {
                return NotFound();
            }

            for (int i = 0; i < chitietdh.Count; i++)
            {
                ketnoidatabase.ChiTietDonHang.Remove(chitietdh[i]);
            }

            await ketnoidatabase.SaveChangesAsync();

            return Ok(chitietdh);
        }

        [HttpDelete("XoaChiTietDonHang/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<IActionResult> XoaChiTietDonHang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chitietdonhang = await ketnoidatabase.ChiTietDonHang.SingleOrDefaultAsync(m => m.Id == id);
            if (chitietdonhang == null)
            {
                return NotFound();
            }

            ketnoidatabase.ChiTietDonHang.Remove(chitietdonhang);

            await ketnoidatabase.SaveChangesAsync();

            return Ok(chitietdonhang);
        }

        private bool KiemTraSuTonTaiCuaChiTietDonHang(int id)
        {
            return ketnoidatabase.ChiTietDonHang.Any(e => e.Id == id);
        }
    }
}