using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.webui.EmailServices;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly SignInManager<ApplicationUser>? signInManager; 
        private readonly RoleManager<IdentityRole>? roleManager; 
        private readonly IEmailSender? emailSender;

        public AccountController(UserManager<ApplicationUser>? _userManager,SignInManager<ApplicationUser>? _signInManager,RoleManager<IdentityRole> _roleManager,IEmailSender? _emailSender)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            emailSender = _emailSender;
        }

        // public async Task<IActionResult> Register()
        // {

            
        //     var user1 = new ApplicationUser()
        //     {
        //         FirstName = "Admin",
        //         LastName = "Admin",
        //         UserName = "Admin",
        //         Email = "admin@shopapp.com",
        //         EmailConfirmed = true
        //     };

        //     var result = await userManager!.CreateAsync(user1,"Shopapp_123");

        //     var result2 = await roleManager!.CreateAsync(new IdentityRole(){Name="Admin"});

        //     await userManager.AddToRoleAsync(user1,"Admin");

        //     var user = new ApplicationUser()
        //     {
        //         FirstName = "Customer",
        //         LastName = "Customer",
        //         UserName = "Customer",
        //         Email = "customer@shopapp.com",
        //         EmailConfirmed = true
        //     };

        //     var result3 = await userManager!.CreateAsync(user,"Shopapp_123");

        //     var result4 = await roleManager!.CreateAsync(new IdentityRole(){Name="Customer"});

        //     await userManager.AddToRoleAsync(user,"Customer");

        //     return View();
            
        // }
        
        [HttpGet]
        public  IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName, 
                Email = model.Email,
            };

            var result = await userManager!.CreateAsync(user,model.Password!);

            if(result.Succeeded)
            {
                TempData["InfoMessage"] =$"Üyelik oluşturuldu.";
                TempData["InfoMessageDesc"] =$"Üyeliğinizi onaylamak için mail adresinizi kontrol edin.";
                TempData["InfoMessageCss"] ="warning";

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = Url.Action("ConfirmEmail", "Account", new {
                    token = token,
                    userId = user.Id
                });

                await emailSender!.SendEmailAsync(user.Email!,"Üyelik Onayı.",$"Hesabınızı onaylamak için lütfen <a href='http://localhost:5182/{url}'>linke</a> tıklayınız");

                return RedirectToAction("Login");

            }

            ModelState.AddModelError("","Bilinmeyen bir hata oldu lütfen tekrar deneyiniz.");

            return View(model);
        }

        [HttpGet]
        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager!.FindByIdAsync(model.Email!);

            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string token,string userId)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {

                TempData["InfoMessage"] =$"Geçersiz Token.";
                TempData["InfoMessageCss"] ="danger";

                return View();
            }

            var user = await userManager!.FindByIdAsync(userId);

            if(user == null)
            {
                TempData["InfoMessage"] =$"Hesabınız onaylanmadı.";
                TempData["InfoMessageCss"] ="danger";

                return View();
            }

            var result = await userManager.ConfirmEmailAsync(user,token);

            if(result.Succeeded)
            {
                TempData["InfoMessage"] =$"Hesabınız onaylandı.";
                TempData["InfoMessageDesc"] =$"Griş yapabilirsiniz.";
                TempData["InfoMessageCss"] ="succsess";

                return RedirectToAction("Login");
            }
            
            return View();
        }

    }
}