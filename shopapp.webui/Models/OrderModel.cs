using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [Display(Name ="Ad")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [Display(Name ="Soyad")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Adres alanı zorunludur.")]
        [Display(Name ="Adres")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Şehir alanı zorunludur.")]
        [Display(Name ="Şehir")]
        public string? City { get; set; }
        
        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Display(Name ="Telefon")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        public string? Note { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Kart Üzerindeki Ad zorunludur.")]
        [Display(Name ="Kart Üzerindeki Ad")]
        public string? CardName { get; set; }

        [Required(ErrorMessage = "Kart Numarası zorunludur.")]
        [Display(Name ="Kart Numarası")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Son Kullanma Ayı zorunludur.")]
        [Display(Name ="Son Kullanma Ayı")]
        public string? ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Son Kullanma Yılı zorunludur.")]
        [Display(Name ="Son Kullanma Yılı")]
        public string? ExpirationYear { get; set; }

        [Required(ErrorMessage = "Güvenlik Numarası zorunludur.")]
        [Display(Name ="Güvenlik Numarası")]
        public string? Cvc { get; set; }
        public CartModel? CartModel { get; set; }


        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
    }
}