using Microsoft.AspNetCore.Identity;
using BooksShoppingCartMvcUI.Constants;
using System;
using System.Threading.Tasks;

namespace BooksShoppingCartMvcUI.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            // Add roles to DB if not exist
                await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            //Create admin user 
            var admin = new IdentityUser
            {
                UserName= "admin@gmail.com",
                Email="admin@gmail.com",
                EmailConfirmed=true

            };
            var userInDb= await userMgr.FindByEmailAsync(admin.Email);
            if(userInDb==null)
            {
               await userMgr.CreateAsync(admin,"Admin@123");
               await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
              
        }
    }
}
