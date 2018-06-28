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
    [Route("api/TaiKhoan")]
    public class TaiKhoanController : Controller
    {
        private readonly UserManager<NguoiDungEntity> _quanlyTaiKhoan;
        private readonly SignInManager<NguoiDungEntity> _quanlyDangNhap;
        private readonly RoleManager<IdentityRole> _quanlyQuyen;
        private IPasswordHasher<NguoiDungEntity> _makhoaMatKhau;
        private ILogger<TaiKhoanController> _logger;
        private IConfiguration _configurationRoot;

        public TaiKhoanController(UserManager<NguoiDungEntity> quanlyTaiKhoan, SignInManager<NguoiDungEntity> quanlyDangNhap, RoleManager<IdentityRole> quanlyQuyen,
            IPasswordHasher<NguoiDungEntity> makhoaMatKhau, ILogger<TaiKhoanController> logger, IConfiguration configurationRoot)
        {
            _quanlyTaiKhoan = quanlyTaiKhoan;
            _quanlyDangNhap = quanlyDangNhap;
            _quanlyQuyen = quanlyQuyen;
            _makhoaMatKhau = makhoaMatKhau;
            _logger = logger;
            _configurationRoot = configurationRoot;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("DangKy")]
        public async Task<IActionResult> Register([FromBody] ModelDangKyMau registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new NguoiDungEntity()
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Ho = registerModel.Ho,
                Ten = registerModel.Ten
            };
            //Tao tai khoan user bang CreateAsync
            var result = await _quanlyTaiKhoan.CreateAsync(user, registerModel.MatKhau);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("error", err.Description);

            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] ModelDangNhapMau model)
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
                    var user = await _quanlyTaiKhoan.FindByNameAsync(model.Email);
                    //neu tim khong co
                    if (user == null)
                    {
                        return BadRequest("Tai Khoan Khong Ton Tai");
                    }
                    //Nếu tìm thấy
                    //So sánh mật khẩu có bằng nhau ko
                    if (_makhoaMatKhau.VerifyHashedPassword(user, user.PasswordHash, model.MatKhau) == PasswordVerificationResult.Success)
                    {
                        var userClaim = await _quanlyTaiKhoan.GetClaimsAsync(user);
                        _logger.LogInformation(userClaim.ToString());

                        var claims = new[]
                        {
                           new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                           new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        }.Union(userClaim);

                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var jwt = new JwtSecurityToken(
                            issuer: _configurationRoot["JwtSecurityToken:Issuer"],
                            audience: _configurationRoot["JwtSecurityToken:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: signingCredentials
                            );

                        var isAdmin = false;

                        if (userClaim.Count > 0)
                        {
                            for (int i = 0; i < userClaim.Count; i++)
                            {
                                if (userClaim[i].Type == "Admin")
                                {
                                    isAdmin = true;
                                }
                                else
                                {
                                    isAdmin = false;
                                }
                            }
                        }
                        else
                        {
                            isAdmin = false;
                        }



                        return Ok(new
                        {
                            email = user.Email,
                            fullname = user.TenHienThi,
                            isadmin = isAdmin,
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
                    _logger.LogError($"Error when creating token: { e }");
                    return StatusCode((int)HttpStatusCode.InternalServerError, $"Error when creating token: {e}");
                }

            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme)]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                //var userName = HttpContext.User.Identity.Name;

                await _quanlyDangNhap.SignOutAsync();

                return Ok("Logout successfully!");
            }
        }
    }
}
