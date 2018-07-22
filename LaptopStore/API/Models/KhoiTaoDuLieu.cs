using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class KhoiTaoDuLieu
    {
        const string taikhoancuaadmin = "taikhoancuaadmin";
        const string matkhaucuaadmin = "matkhaucuaadmin";

        public static async Task KhoiTaoDuLieuBanDau (IServiceProvider serviceProvider, bool createUser = true)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<LapTopStoreContext>();
                var result = await db.Database.EnsureCreatedAsync();
                if (result)
                {
                    if (createUser == true)
                    {
                        await TaoTaiKhoanAdmin(scopeServiceProvider);
                    }
                }
                
            }
        }

        private static async Task TaoTaiKhoanAdmin(IServiceProvider serviceProvider)
        {
            //Lấy đối tượng môi trường đang chạy App
            var env = serviceProvider.GetService<IHostingEnvironment>();
            
            var cauhinhmoitruong = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath)
                              .AddJsonFile("appsettings.json")
                              .AddEnvironmentVariables();
            var cauhinh = cauhinhmoitruong.Build();
            //Khởi tạo lớp hỗ trợ tạo tài khoãn
            var quanlynguoidung = serviceProvider.GetService<UserManager<NguoiDungEntity>>();
            //Kiểm tra có tài khoản admin chưa
            var user = await quanlynguoidung.FindByNameAsync(cauhinh[taikhoancuaadmin]);

            if (user == null)
            {
                var taikhoanadmin = new NguoiDungEntity() {
                    UserName = cauhinh[taikhoancuaadmin],
                    Email = cauhinh[taikhoancuaadmin]  
                };
                await quanlynguoidung.CreateAsync(taikhoanadmin, cauhinh[matkhaucuaadmin]);
                await quanlynguoidung.AddClaimAsync(taikhoanadmin, new Claim("Admin", "Allowed"));
            }
        }
    }
}