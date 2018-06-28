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
        const string defaultAdminUserName = "DefaultAdminUserName";
        const string defaultAdminPassword = "DefaultAdminPassword";

        public static async Task InitializeLaptopStoreDatabaseAsync (IServiceProvider serviceProvider, bool createUser = true)
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
                        await CreateAdminUser(scopeServiceProvider);
                    }
                }
                
            }
        }

        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<LapTopStoreContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<LapTopStoreContext>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }

        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath)
                              .AddJsonFile("appsettings.json")
                              .AddEnvironmentVariables();
            var configuration = builder.Build();

            var userManager = serviceProvider.GetService<UserManager<NguoiDungEntity>>();

            var user = await userManager.FindByNameAsync(configuration[defaultAdminUserName]);

            if (user == null)
            {
                var createUser = new NguoiDungEntity() {
                    UserName = configuration[defaultAdminUserName],
                    Email = configuration[defaultAdminUserName]  
                };
                await userManager.CreateAsync(createUser, configuration[defaultAdminPassword]);
                await userManager.AddClaimAsync(createUser, new Claim("Admin", "Allowed"));
            }
        }
    }
}