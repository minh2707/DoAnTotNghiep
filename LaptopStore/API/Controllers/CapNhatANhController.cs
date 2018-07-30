using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/CapNhatAnh")]
    public class CapNhatANhController : Controller
    {
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Administrations")]
        public async Task<IActionResult> CapNhatHinhAnh(IFormFile taptin)
        {
            long dolon = taptin.Length;

            
            var duongdantaptin = Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "git\\DoAnTotNghiep\\LaptopStore\\WebCore\\wwwroot\\img\\sanpham", taptin.FileName);

            if (dolon > 0)
            {
                using (var stream = new FileStream(duongdantaptin, FileMode.Create))
                {
                    await taptin.CopyToAsync(stream);
                }
            }

            
            return Ok(new { taptin = taptin, dolon, duongdantaptin });
        }
    }
}