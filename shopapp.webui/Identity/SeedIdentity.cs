using Microsoft.AspNetCore.Identity;
using shopapp.business.Abstract;

namespace shopapp.webui.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,ICartService cartService,IConfiguration configuration)
        {           
            var admin = new ApplicationUser()
            {
                FirstName = configuration["Identity:Admin:FirstName"],
                LastName = configuration["Identity:Admin:LastName"],
                UserName = configuration["Identity:Admin:UserName"],
                Email = configuration["Identity:Admin:Email"],
                EmailConfirmed = true
            };

            var adminUserExist = await userManager.FindByNameAsync(configuration["Identity:Admin:UserName"]!);

            if(adminUserExist == null)
            {
                await userManager!.CreateAsync(admin,configuration["Identity:Admin:Password"]!);
                await cartService.InitializeCartAsync(admin.Id);
            }

            var adminRoleExist = await roleManager.FindByNameAsync(configuration["Identity:Admin:Role"]!);

            if(adminRoleExist == null)
            {
                await roleManager!.CreateAsync(new IdentityRole(){Name = configuration["Identity:Admin:Role"]!});
            }

            var adminInRole = await userManager.IsInRoleAsync(admin,configuration["Identity:Admin:Role"]!);

            if(!adminInRole)
            {
                await userManager.AddToRoleAsync(admin,configuration["Identity:Admin:Role"]!);
            }

            var user = new ApplicationUser()
            {
                FirstName = configuration["Identity:Customer:FirstName"],
                LastName = configuration["Identity:Customer:LastName"],
                UserName = configuration["Identity:Customer:UserName"],
                Email = configuration["Identity:Customer:Email"],
                EmailConfirmed = true
            };

            var CustomerUserExist = await userManager.FindByNameAsync(configuration["Identity:Customer:UserName"]!);

            if(CustomerUserExist == null)
            {
                await userManager!.CreateAsync(user,configuration["Identity:Customer:Password"]!);
                await cartService.InitializeCartAsync(user.Id);
            }

            var customerRoleExist = await roleManager.FindByNameAsync(configuration["Identity:Customer:Role"]!);

            if(customerRoleExist == null)
            {
                await roleManager!.CreateAsync(new IdentityRole(){Name = configuration["Identity:Customer:Role"]!});
            }

            var customerInRole = await userManager.IsInRoleAsync(user,configuration["Identity:Customer:Role"]!);

            if(!customerInRole)
            {
                await userManager.AddToRoleAsync(user,configuration["Identity:Customer:Role"]!);
            }
        }
    }
}