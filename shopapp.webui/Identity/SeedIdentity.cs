using Microsoft.AspNetCore.Identity;

namespace shopapp.webui.Identity
{

    
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {           
            var admin = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin",
                Email = "admin@shopapp.com",
                EmailConfirmed = true
            };

            var result = await userManager!.CreateAsync(admin,"Shopapp_123");

            var result2 = await roleManager!.CreateAsync(new IdentityRole(){Name="Admin"});

            await userManager.AddToRoleAsync(admin,"Admin");

            var user = new ApplicationUser()
            {
                FirstName = "Customer",
                LastName = "Customer",
                UserName = "Customer",
                Email = "customer@shopapp.com",
                EmailConfirmed = true
            };

            var result3 = await userManager!.CreateAsync(user,"Shopapp_123");

            var result4 = await roleManager!.CreateAsync(new IdentityRole(){Name="Customer"});

            await userManager.AddToRoleAsync(user,"Customer");
        }
    }
}