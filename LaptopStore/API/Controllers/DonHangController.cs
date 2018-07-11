using System;
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

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/DonHang")]
    public class OrdersController : Controller
    {
        private readonly LapTopStoreContext _context;

        public OrdersController(LapTopStoreContext context)
        {
            _context = context;
        }

        // GET: api/LayHetDonHang lay het tat don hang
        [HttpGet("api/LayHetDonHang")]
        public IEnumerable<DonHang> LayTatCaDonHang()
        {
            return _context.DonHang;
        }

        // GET: api/LayMotDonHang/5
        [HttpGet("api/LayMotDonHang/{id}")]
        public async Task<IActionResult> LayMotDonHang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.DonHang.SingleOrDefaultAsync(m => m.Id== id);

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

            _context.Entry(donhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            _context.DonHang.Add(donhang);
            var kq = await _context.SaveChangesAsync();

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

            var donhang = await _context.DonHang.SingleOrDefaultAsync(m => m.Id == id);
            if (donhang == null)
            {
                return NotFound();
            }

            _context.DonHang.Remove(donhang);
            await _context.SaveChangesAsync();

            return RedirectToAction("XoaChiTietDonHang", "ChiTietDonHang", new { id = id});
        }

        // GET: api/Orders/5/OrderDetails
        [HttpGet]
        [Route("{id}/OrderDetails")]
        public async Task<IActionResult> GetDetailOfOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chitietdonhang = await _context.ChiTietDonHang.Where(p => p.Id== id).ToListAsync();

            return Ok(chitietdonhang);
        }

        private bool Kiemtrasutontaicuadonhang(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }

    }
}