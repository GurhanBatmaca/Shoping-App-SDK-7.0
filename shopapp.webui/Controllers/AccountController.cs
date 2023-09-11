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
            if(User.Identity!.IsAuthenticated!)
            {
                return RedirectToAction("Index","Home");
            }

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

                await emailSender!.SendEmailAsync(user.Email!,"Üyelik Onayı.",$"Hesabınızı onaylamak için lütfen <a href='http://localhost:5182{url}'>linke</a> tıklayınız");

                return RedirectToAction("Login");

            }

            ModelState.AddModelError("","Bilinmeyen bir hata oldu lütfen tekrar deneyiniz.");

            return View(model);
        }

        [HttpGet]
        public  IActionResult Login()
        {
            if(User.Identity!.IsAuthenticated!)
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager!.FindByEmailAsync(model.Email!);

            if(user == null)
            {
                TempData["InfoMessage"] =$"Giriş yapılamadı.";
                TempData["InfoMessageDesc"] ="Kullanıcı kayıdı bulunamadı.";
                TempData["InfoMessageCss"] ="danger";

                return View(model);
            }

            if(!await userManager.IsEmailConfirmedAsync(user))
            {
                TempData["InfoMessage"] =$"Giriş yapılamadı.";
                TempData["InfoMessageDesc"] ="Lütfen email adresinizi onaylayın.";
                TempData["InfoMessageCss"] ="danger";

                ModelState.AddModelError("","Email hesabınızı onaylayınız");
                return View(model);
            }

            var result = await signInManager!.PasswordSignInAsync(user,model.Password!,true,false);

            if(result.Succeeded)
            {
                TempData["InfoMessage"] =$"Hoşgeldin {user.UserName}.";
                TempData["InfoMessageCss"] ="success";

                return Redirect("~/");
            }
            
            ModelState.AddModelError("","Girilen kullancı adı veya parola yanlış.");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if(!User.Identity!.IsAuthenticated!)
            {
                return RedirectToAction("Index","Home");
            }
            
            await signInManager!.SignOutAsync();

            TempData["InfoMessage"] =$"Oturum kapatıldı.";
            TempData["InfoMessageDesc"] ="Güvenli şekilde çıkış yapıldı.";
            TempData["InfoMessageCss"] ="warning";

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
                TempData["InfoMessageDesc"] =$"Giriş yapabilirsiniz.";
                TempData["InfoMessageCss"] ="success";

                return RedirectToAction("Login");
            }
            
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager!.FindByEmailAsync(model.Email!);

            if(user == null)
            {
                ModelState.AddModelError("","Girilen email adresine ait kullanıcı bulunamadı.");

                return View(model);
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new {
                token = token,
                userId = user.Id
            });

            await emailSender!.SendEmailAsync(model.Email!,"Parola sıfırlama.",$"Şifrenizi yenilemek için lütfen <a href='http://localhost:5182{url}'>linke</a> tıklayın.");

            TempData["InfoMessage"] =$"Parolanızı sıfırlamak için mail adresinizi kontrol edin..";
            TempData["InfoMessageCss"] ="warning";

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token,string userId)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                TempData["InfoMessage"] =$"Geçersiz Token.";
                TempData["InfoMessageCss"] ="danger";

                ModelState.AddModelError("","Geçersiz Token.");

                return RedirectToAction("ForgotPassword");
            }

            var resetPasswordModel = new ResetPasswordModel()
            {
                Token = token,
            };

            return View(resetPasswordModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if(!ModelState.IsValid)
            {               
                return View(model);
            }

            var user = await userManager!.FindByEmailAsync(model.Email!);

            if(user == null)
            {
                TempData["InfoMessage"] =$"Mail adresi hatası.";
                TempData["InfoMessageCss"] ="danger";

                ModelState.AddModelError("","Mail adresi bulunamadı.");

                return View(model);
            }

            var result = await userManager.ResetPasswordAsync(user,model.Token!,model.Password!);

            if(result.Succeeded)
            {
                TempData["InfoMessage"] =$"Yeni şifre kaydedildi.";
                TempData["InfoMessageCss"] ="success";

                return RedirectToAction("Login");
            }

            ModelState.AddModelError("","Şifre en az 6 karater uzunluğunda olmalı,büyük ve küçük harf,rakam ve alfanumerik karakter içermelidir.");

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    
    }
}