using System.ComponentModel.DataAnnotations;

namespace Crud.Frontend.Models
{
    public class ProductModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="Name is required")]
        [MaxLength(10, ErrorMessage = "Cannot be more than 10 characters")]
        [MinLength(3, ErrorMessage = "Cannot be less than 3 characters")]
        public string Name { get; set; }


        [Range(1, double.MaxValue, ErrorMessage ="you should insert a decimal number")]
        public decimal Price { get; set; }
    }
}
