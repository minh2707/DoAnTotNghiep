using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration conf, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = conf;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
            // Mở CORS Domain với mọi phương thức
            services.AddCors(o => o.AddPolicy("MyPolicyA", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            //Cấu hình đường dẫn kết nói DB
            var connection = @"Data Source=MINH;Initial Catalog=LapTopStore;Integrated Security=True";
            //Kết nối DB với đường dẫn
            services.AddDbContext<LapTopStoreContext>(tuychon => tuychon.UseSqlServer(connection, sqlOptions => sqlOptions.MigrationsAssembly("API")));

            /*Thêm các bảng user vào database được hỗ trợ bở ASP.Net*/
            services.AddIdentity<NguoiDungEntity, IdentityRole>()
                    .AddEntityFrameworkStores<LapTopStoreContext>()
                    .AddDefaultTokenProviders();

            // Cấu hình phân quyền user
            services.AddAuthorization(tuychon =>
            {
                tuychon.AddPolicy(
                    "Administrations",
                    authBuilder =>
                    {
                        authBuilder.RequireClaim("Admin", "Allowed");
                    });
            });

            // Mở chức năng Xác thực người dùng của ASP và Gán phương thức xác thực mặc định là JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme;
            })
            //Mở Cookie
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, tuychon =>
            {
                tuychon.SlidingExpiration = true;
            })
            /*Cấu hình API sử dụng phương thức xác thực bằng JWT */
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, tuychon =>
            {
                tuychon.RequireHttpsMetadata = false;
                tuychon.SaveToken = true;

                tuychon.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
                    ValidAudience = Configuration["JwtSecurityToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtSecurityToken:Key"])),
                    ClockSkew = TimeSpan.FromMinutes(30)
                };

                tuychon.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = cauhinh =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " +
                            cauhinh.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " +
                            context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Mở chức năng CORS Domain
            app.UseCors("MyPolicyA");
            //Thêm chức năng xác thực người dùng
            app.UseAuthentication();

            KhoiTaoDuLieu.KhoiTaoDuLieuBanDau(app.ApplicationServices).Wait();

            app.UseMvc();
        }
    }
}
