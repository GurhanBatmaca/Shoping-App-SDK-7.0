using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class CategoryModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Kategori adı alanı zorunludur.")]
        [Display(Name = "Kategori adı")]
        public string? Name { get; set; }
        public string? Url { get; set; }
         
    }
}