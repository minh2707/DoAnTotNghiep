﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API.Controllers
{
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private readonly LapTopStoreContext ketnoidatabase;

        public OrdersController(LapTopStoreContext ketnoi)
        {
            ketnoidatabase = ketnoi;
        }

        // GET: api/LayHetDonHang lay het tat don hang
        [HttpGet("api/LayHetDonHang")]
        public IEnumerable<DonHang> LayTatCaDonHang()
        {
            return ketnoidatabase.DonHang;
        }

        // GET: api/LayMotDonHang/5
        [HttpGet("api/LayMotDonHang/{id}")]
        public async Task<IActionResult> LayMotDonHang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await ketnoidatabase.DonHang.SingleOrDefaultAsync(m => m.Id== id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/CapNhatDonHang/5
        [HttpPut("api/CapNhatDonHang/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<IActionResult> CapNhatDonHang([FromRoute] int id, [FromBody] DonHang donhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donhang.Id)
            {
                return BadRequest();
            }

            ketnoidatabase.Entry(donhang).State = EntityState.Modified;

            try
            {
                await ketnoidatabase.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Kiemtrasutontaicuadonhang(id))
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

        // POST: api/TaoDonHang
        [HttpPost("api/TaoDonHang")]
        public async Task<IActionResult> TaoDonHang([FromBody] DonHang donhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ketnoidatabase.DonHang.Add(donhang);
            var kq = await ketnoidatabase.SaveChangesAsync();

            return CreatedAtAction("LayMotDonHang", new { id = donhang.Id}, donhang);

        }

        // DELETE: api/XoaDonHang/5
        [HttpDelete("api/XoaDonHang/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<IActionResult> XoaDonHang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var donhang = await ketnoidatabase.DonHang.SingleOrDefaultAsync(m => m.Id == id);
            if (donhang == null)
            {
                return NotFound();
            }

            ketnoidatabase.DonHang.Remove(donhang);
            await ketnoidatabase.SaveChangesAsync();

            return RedirectToAction("XoaChiTietDonHangBangIdDonHang", "ChiTietDonHang", new { iddonhang = id});
        }

        [HttpGet]
        [Route("api/LayChiTietDonHangCuaDonHang/{id}")]
        public async Task<IActionResult> LayChiTietDonHangCuaDonHang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chitietdonhang = await ketnoidatabase.ChiTietDonHang.Where(p => p.IddonHang== id).ToListAsync();

            return Ok(chitietdonhang);
        }

        private bool Kiemtrasutontaicuadonhang(int id)
        {
            return ketnoidatabase.DonHang.Any(e => e.Id == id);
        }

    }
}