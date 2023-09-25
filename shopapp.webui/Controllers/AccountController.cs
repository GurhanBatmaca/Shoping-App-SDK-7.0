using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.webui.EmailServices;
using shopapp.webui.Extentions;
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
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser>? _userManager,SignInManager<ApplicationUser>? _signInManager,RoleManager<IdentityRole> _roleManager,IEmailSender? _emailSender,IConfiguration _configuration)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            emailSender = _emailSender;
            configuration = _configuration;
        }
        
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

            var nameExist = await userManager!.FindByNameAsync(model.UserName!);
            var emailExist = await userManager.FindByEmailAsync(model.Email!);

            if(nameExist != null)
            {
                TempData.Put("message",new InfoMessage
                {
                    Title = "Bu isimle daha önce kullanıcı oluşturulmuş.",
                    Message = "Lütfen farklı bir kullanıcı adı seçin.",
                    AlertType = "danger"
                }); 

                return View(model);
            }
            if(emailExist != null)
            {
                TempData.Put("message",new InfoMessage
                {
                    Title = "Bu mail adresi ile daha önce kullanıcı oluşturulmuş.",
                    Message = "Şifrenizi unuttuysanız şifremi unuttum kısmından yeniden alabilirsiniz.",
                    AlertType = "danger"
                }); 

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
                TempData.Put("message",new InfoMessage
                {
                    Title = "Üyelik oluşturuldu.",
                    Message = "Üyeliğinizi onaylamak için mail adresinizi kontrol edin.",
                    AlertType = "warning"
                }); 

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
                TempData.Put("message",new InfoMessage
                {
                    Title = "Giriş yapılamadı.",
                    Message = $"Kullanıcı kayıdı bulunamadı.",
                    AlertType = "danger"
                }); 

                return View(model);
            }

            if(!await userManager.IsEmailConfirmedAsync(user))
            {
                TempData.Put("message",new InfoMessage
                {
                    Title = "Giriş yapılamadı.",
                    Message = $"Lütfen email adresinizi onaylayın.",
                    AlertType = "danger"
                }); 

                ModelState.AddModelError("","Email hesabınızı onaylayınız");
                return View(model);
            }

            var result = await signInManager!.PasswordSignInAsync(user,model.Password!,true,false);

            if(result.Succeeded)
            {
                TempData.Put("message",new InfoMessage
                {
                    Title = $"Hoşgeldin {user.UserName}.",
                    AlertType = "success"
                });

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

            TempData.Put("message",new InfoMessage
            {
                    Title = "Oturum kapatıldı.",
                    Message = $"Güvenli şekilde çıkış yapıldı.",
                    AlertType = "warning"
            }); 

            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string token,string userId)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {

                TempData.Put("message",new InfoMessage
                {
                        Title = "Geçersiz Token.",
                        AlertType = "danger"
                });

                return View();
            }

            var user = await userManager!.FindByIdAsync(userId);

            if(user == null)
            {
                TempData.Put("message",new InfoMessage
                {
                    Title = "Geçersiz kullanıcı.",
                    AlertType = "danger"
                });

                return View();
            }

            var result = await userManager.ConfirmEmailAsync(user,token);

            if(result.Succeeded)
            {

                TempData.Put("message",new InfoMessage
                {
                    Title = "Hesabınız onaylandı.",
                    Message = $"Giriş yapabilirsiniz.",
                    AlertType = "success"
                }); 

                await userManager.AddToRoleAsync(user,configuration["Identity:Customer:Role"]!);

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

            TempData.Put("message",new InfoMessage
            {
                Title = "Parolanızı sıfırlamak için mail adresinizi kontrol edin.",
                AlertType = "warning"
            }); 

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token,string userId)
        {
            if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {

                TempData.Put("message",new InfoMessage
                {
                    Title = "Geçersiz Token.",
                    AlertType = "danger"
                });

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
                TempData.Put("message",new InfoMessage
                {
                    Title = "Mail adresi hatası.",
                    AlertType = "danger"
                });

                ModelState.AddModelError("","Mail adresi bulunamadı.");

                return View(model);
            }

            var result = await userManager.ResetPasswordAsync(user,model.Token!,model.Password!);

            if(result.Succeeded)
            {
                TempData.Put("message",new InfoMessage
                {
                    Title = "Yeni şifre kaydedildi.",
                    AlertType = "success"
                });

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