using System.ComponentModel.DataAnnotations;

namespace MiniBay.Shared.Feature.Products
{
    public class CreateProductsDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nam_Pro { get; set; }

        [Required(ErrorMessage = "Es obligatorio poner un descripcion")]
        [MaxLength(550)]
        public string Des_Pro { get; set; }

        [Required]
        [Range(1.00, (double)decimal.MaxValue, ErrorMessage = "El precio debe ser de al menos $1.00")]
        public decimal Pri_Pro { get; set; }

        public String? Url_Pro { get; set; }
    }
}
