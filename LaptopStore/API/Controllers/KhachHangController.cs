using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class KhachHangController : Controller
    {
        private readonly LapTopStoreContext ketnoidatabase;

        public KhachHangController(LapTopStoreContext ketnoi)
        {
            ketnoidatabase = ketnoi;
        }

        // GET: api/Customers
        [HttpGet("LayHetKhachHang")]
        public IEnumerable<KhachHang> LayHetKhachHang()
        {
            return ketnoidatabase.KhachHang;
        }

        // GET: api/Customers/5
        [HttpGet("LayMotKhachHang/{id}")]
        public async Task<IActionResult> LayMotKhachHang([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kh = await ketnoidatabase.KhachHang.SingleOrDefaultAsync(m => m.Id == id);

            if (kh == null)
            {
                return NotFound();
            }

            return Ok(kh);
        }

        // PUT: api/Customers/5
        [HttpPut("CapNhatKhachHang/{id}")]
        public async Task<IActionResult> CapNhatKhachHang([FromRoute] string id, [FromBody] KhachHang khachhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khachhang.Id)
            {
                return BadRequest();
            }

            ketnoidatabase.Entry(khachhang).State = EntityState.Modified;

            try
            {
                await ketnoidatabase.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KtKhachHangTonTai(id))
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

        // POST: api/Customers
        [HttpPost]
        [Route("TaoKhachHang")]
        public JsonResult TaoKhachHang([FromBody] KhachHang kh)
        {
            var cus = ketnoidatabase.KhachHang.Find(kh.Id);
            if (cus != null)
            {
                throw new Exception("Tồn Tại Khách Hàng Trong Hệ Thống");
            }
            ketnoidatabase.KhachHang.Add(kh);
            ketnoidatabase.SaveChanges();
            return Json(kh);
        }

        // DELETE: api/Customers/5
        [HttpDelete("XoaKhachHang/{id}")]
        public async Task<IActionResult> XoaKhachHang([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kh = await ketnoidatabase.KhachHang.SingleOrDefaultAsync(m => m.Id == id);
            if (kh == null)
            {
                return NotFound();
            }

            ketnoidatabase.KhachHang.Remove(kh);
            await ketnoidatabase.SaveChangesAsync();

            return Ok(kh);
        }

        private bool KtKhachHangTonTai(string id)
        {
            return ketnoidatabase.KhachHang.Any(e => e.Id == id);
        }
    }
}