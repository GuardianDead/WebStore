using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class ProductModelPhoto
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Фото")]
        public byte[] Value { get; set; }

        [DisplayName("Модель")]
        public string ProductModelId { get; set; }
        [Required]
        [DisplayName("Модель")]
        [Display(AutoGenerateField = false)]
        public ProductModel ProductModel { get; }

        public ProductModelPhoto()
        {
        }

        public ProductModelPhoto(byte[] value)
        {
            Value = value;
        }

        public override string ToString() => Id.ToString();
    }
}
