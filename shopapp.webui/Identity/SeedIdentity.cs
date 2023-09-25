using Microsoft.AspNetCore.Identity;

namespace shopapp.webui.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {           
            var admin = new ApplicationUser()
            {
                FirstName = configuration["Identity:Admin:FirstName"],
                LastName = configuration["Identity:Admin:LastName"],
                UserName = configuration["Identity:Admin:UserName"],
                Email = configuration["Identity:Admin:Email"],
                EmailConfirmed = true
            };

            var result = await userManager!.CreateAsync(admin,configuration["Identity:Admin:Password"]!);

            var result2 = await roleManager!.CreateAsync(new IdentityRole(){Name="Admin"});

            await userManager.AddToRoleAsync(admin,"Admin");

            var user = new ApplicationUser()
            {
                FirstName = configuration["Identity:Customer:FirstName"],
                LastName = configuration["Identity:Customer:LastName"],
                UserName = configuration["Identity:Customer:UserName"],
                Email = configuration["Identity:Customer:Email"],
                EmailConfirmed = true
            };

            var result3 = await userManager!.CreateAsync(user,configuration["Identity:Customer:Password"]!);

            var result4 = await roleManager!.CreateAsync(new IdentityRole(){Name="Customer"});

            await userManager.AddToRoleAsync(user,"Customer");
        }
    }
}