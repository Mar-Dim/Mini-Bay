using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBay.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id_Pro { get; set; }

        [Required]
        [MaxLength(100)]
        public String Nam_Pro { get; set; }
        [Required]
        [MaxLength(550)]
        public String Des_Pro { get; set; }
        [Required]
        [Range(1.00, (double)decimal.MaxValue, ErrorMessage = "El precio debe ser de al menos $1.00")]
        public decimal Pri_Pro { get; set; }
        public String Url_Pro { get; set; }
    }
}
