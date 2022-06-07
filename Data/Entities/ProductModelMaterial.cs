using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class ProductModelMaterial
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Модель")]
        public string ProductModelId { get; set; }
        [Required]
        [DisplayName("Модель")]
        [Display(AutoGenerateField = false)]
        public ProductModel ProductModel { get; }

        public ProductModelMaterial()
        {
        }

        public ProductModelMaterial(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
