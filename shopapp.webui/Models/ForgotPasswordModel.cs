using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage ="Email alanı zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
         
    }
}