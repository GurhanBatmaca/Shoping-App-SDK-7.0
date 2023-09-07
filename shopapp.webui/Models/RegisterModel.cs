using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Kullanıcı adı alanı zorunludur.")]
        [Display(Name = "Kullanıcı adı")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="İsim alanı zorunludur.")]
        [Display(Name = "İsim")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage ="Soyisim alanı zorunludur.")]
        [Display(Name = "Soyisim")]
        public string? LastName { get; set; }

        [Required(ErrorMessage ="Email alanı zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Şifre alanı zorunludur.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage ="Şifre alanı zorunludur.")]
        [Display(Name = "Şifre Tekrarı")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifre tekrarı aynı değil.")]
        public string? RePassword { get; set; }
         
    }
}