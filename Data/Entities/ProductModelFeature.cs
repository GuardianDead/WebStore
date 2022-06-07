using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class ProductModelFeature
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Значение")]
        public string Value { get; set; }

        [DisplayName("Модель")]
        public string ProductModelId { get; set; }
        [Required]
        [DisplayName("Модель")]
        [Display(AutoGenerateField = false)]
        public ProductModel ProductModel { get; }

        public ProductModelFeature()
        {
        }

        public ProductModelFeature(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString() => Name;
    }
}
