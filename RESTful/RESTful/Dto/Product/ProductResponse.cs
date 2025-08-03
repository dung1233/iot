using System.ComponentModel.DataAnnotations;
namespace RESTful.Dto.Product
{
    public class ProductResponse
    {
        [Required]
        public string Id { get; set; } 
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } 
    }
}
