using System.ComponentModel.DataAnnotations;

namespace shopapp.webui.Models
{
    public class ResetPasswordModel
    {
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

        public string? Token { get; set; }
    }
}