using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.webui.Identity;

namespace shopapp.webui.Controllers
{
    public class AccontController: Controller
    {
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly SignInManager<ApplicationUser>? signInManager; 

        public AccontController(UserManager<ApplicationUser>? _userManager,SignInManager<ApplicationUser>? _signInManager,RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public  IActionResult Register()
        {

            return View();
            // var user1 = new ApplicationUser()
            // {
            //     FirstName = "Admin",
            //     LastName = "Admin",
            //     UserName = "Admin",
            //     Email = "admin@shopapp.com",
            //     EmailConfirmed = true
            // };

            // var result = await userManager!.CreateAsync(user1,"Shopapp_123");

            // var result2 = await roleManager!.CreateAsync(new IdentityRole(){Name="Admin"});

            // await userManager.AddToRoleAsync(user1,"Admin");

            // var user = new ApplicationUser()
            // {
            //     FirstName = "Customer",
            //     LastName = "Customer",
            //     UserName = "Customer",
            //     Email = "customer@shopapp.com",
            //     EmailConfirmed = true
            // };

            // var result3 = await userManager!.CreateAsync(user,"Shopapp_123");

            // var result4 = await roleManager!.CreateAsync(new IdentityRole(){Name="Customer"});

            // await userManager.AddToRoleAsync(user,"Customer");
            
        }
        
    }
}