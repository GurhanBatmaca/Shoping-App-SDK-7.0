using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ProductDetailsModel
    {
        public Product? Product { get; set; }
        public List<Category>? Categories { get; set; } = new List<Category>();
    }
}