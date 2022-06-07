using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class ProductArticle
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public string Id { get; init; }
        [Required]
        [DisplayName("Размер")]
        public int Size { get; set; }
        [Required]
        [DisplayName("Цвет")]
        public string Color { get; set; }

        [DisplayName("Модель")]
        [Display(AutoGenerateField = false)]
        public string ModelId { get; set; }
        [Required]
        [DisplayName("Модель")]
        public ProductModel Model { get; set; }

        public ProductArticle()
        {
        }
        public ProductArticle(ProductModel model, int size, string color)
        {
            Id = Guid.NewGuid().ToString("N");
            Model = model;
            Size = size;
            Color = color;
        }

        public override string ToString() => $"{Id} - {Model.Name}";
    }
}
