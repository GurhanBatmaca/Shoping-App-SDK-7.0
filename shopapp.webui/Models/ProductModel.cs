using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Ürün adı alanı zorunludur.")]
        [Display(Name = "Ürün adı")]
        public string? Name { get; set; }
        public string? Url { get; set; }

        [Required(ErrorMessage ="Fiyat alanı zorunludur.")]
        [Display(Name = "Fiyat")]
        [Range(1,100000,ErrorMessage = "1-100000 arasında bir rakam giriniz.")]
        public double? Price { get; set; }

        [Required(ErrorMessage ="Açıklama alanı zorunludur.")]
        [Display(Name = "Açıklama")]
        [StringLength(100,MinimumLength=10,ErrorMessage ="Açıklama 10-100 karakter arasında olmalıdır.")]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        [Display(Name = "Onaylı")]
        public bool IsAproved { get; set; }

        [Display(Name = "Anasayfa")]
        public bool IsHome { get; set; }

        [Display(Name = "Popüler")]
        public bool IsPopular{ get; set; }
        
        [Required(ErrorMessage = "Resim Alanı zorunludur.")]
        [Display(Name = "Resim")]
        [DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }
         
    }
}