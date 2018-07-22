using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class TaiKhoanController : Controller
    {
        private readonly UserManager<NguoiDungEntity> _quanlyTaiKhoan;
        private readonly SignInManager<NguoiDungEntity> _quanlyDangNhap;
        private readonly RoleManager<IdentityRole> _quanlyQuyen;
        private IPasswordHasher<NguoiDungEntity> _makhoaMatKhau;
        private ILogger<TaiKhoanController> _logger;
        private IConfiguration _configurationRoot;
        private readonly LapTopStoreContext ketnoidatabase;

        public TaiKhoanController(UserManager<NguoiDungEntity> quanlyTaiKhoan, SignInManager<NguoiDungEntity> quanlyDangNhap, RoleManager<IdentityRole> quanlyQuyen,
            IPasswordHasher<NguoiDungEntity> makhoaMatKhau, ILogger<TaiKhoanController> logger, IConfiguration configurationRoot, LapTopStoreContext ketnoi)
        {
            _quanlyTaiKhoan = quanlyTaiKhoan;
            _quanlyDangNhap = quanlyDangNhap;
            _quanlyQuyen = quanlyQuyen;
            _makhoaMatKhau = makhoaMatKhau;
            _logger = logger;
            _configurationRoot = configurationRoot;
            ketnoidatabase = ketnoi;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("DangKy")]
        public async Task<IActionResult> DangKy([FromBody] ModelDangKyMau taikhoandungdedangky)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var khachhang = new KhachHang()
            {
                Id = Guid.NewGuid().ToString(),
                HoTen = taikhoandungdedangky.Ho + " " + taikhoandungdedangky.Ten,

            };

            await ketnoidatabase.KhachHang.AddAsync(khachhang);
            await ketnoidatabase.SaveChangesAsync();
            var kqTaoKhachHang = ketnoidatabase.KhachHang.Where(p => p.Id == khachhang.Id).SingleOrDefault();

            var taikhoan = new NguoiDungEntity()
            {
                UserName = taikhoandungdedangky.Email,
                Email = taikhoandungdedangky.Email,
                Ho = taikhoandungdedangky.Ho,
                Ten = taikhoandungdedangky.Ten,
                IdKhachHang = kqTaoKhachHang.Id
            };
            //Tao tai khoan user bang CreateAsync
            var kq = await _quanlyTaiKhoan.CreateAsync(taikhoan, taikhoandungdedangky.MatKhau);
            
            if (kq.Succeeded)
            {
                

                return Ok(kq);
            }

            foreach (var err in kq.Errors)
            {
                ModelState.AddModelError("error", err.Description);

            }

            return BadRequest(kq.Errors);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DangNhap")]
        public async Task<IActionResult> DangNhap([FromBody] ModelDangNhapMau dulieu)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    //Tim tai khoan bang email
                    var tk = await _quanlyTaiKhoan.FindByNameAsync(dulieu.Email);
                    //neu tim khong co
                    if (tk == null)
                    {
                        return BadRequest("Tai Khoan Khong Ton Tai");
                    }
                    //Nếu tìm thấy
                    //So sánh mật khẩu có bằng nhau ko
                    if (_makhoaMatKhau.VerifyHashedPassword(tk, tk.PasswordHash, dulieu.MatKhau) == PasswordVerificationResult.Success)
                    {
                        var quyencuauser = await _quanlyTaiKhoan.GetClaimsAsync(tk);
                        _logger.LogInformation(quyencuauser.ToString());

                        var quyenhan = new[]
                        {
                           new Claim(JwtRegisteredClaimNames.Sub, tk.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                           new Claim(JwtRegisteredClaimNames.Email, tk.Email),
                        }.Union(quyencuauser);

                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var jwt = new JwtSecurityToken(
                            issuer: _configurationRoot["JwtSecurityToken:Issuer"],
                            audience: _configurationRoot["JwtSecurityToken:Audience"],
                            claims: quyenhan,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: signingCredentials
                            );

                        var laAdmin = false;

                        if (quyencuauser.Count > 0)
                        {
                            for (int i = 0; i < quyencuauser.Count; i++)
                            {
                                if (quyencuauser[i].Type == "Admin")
                                {
                                    laAdmin = true;
                                }
                                else
                                {
                                    laAdmin = false;
                                }
                            }
                        }
                        else
                        {
                            laAdmin = false;
                        }



                        return Ok(new
                        {
                            email = tk.Email,
                            tenHienThi = tk.TenHienThi,
                            idKhachHang = tk.IdKhachHang,
                            laAdmin = laAdmin,
                            id = tk.Id,
                            token = new JwtSecurityTokenHandler().WriteToken(jwt),
                            expiration = jwt.ValidTo
                        });
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Lỗi: { e }");
                    return StatusCode((int)HttpStatusCode.InternalServerError, $"Lỗi: {e}");
                }

            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme)]
        [Route("DangXuat")]
        public async Task<IActionResult> DangXuat()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                //var userName = HttpContext.User.Identity.Name;

                await _quanlyDangNhap.SignOutAsync();

                return Ok("Đăng Xuất Thành Công!");
            }
        }
    }
}
