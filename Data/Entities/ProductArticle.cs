using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductArticle
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public Guid Id { get; private init; }
        [Required]
        [DisplayName("Модель")]
        public ProductModel Model { get; set; }
        [Required]
        [DisplayName("Размер")]
        public int Size { get; set; }
        [Required]
        [DisplayName("Цвет")]
        public string Color { get; set; }

        public ProductArticle()
        {
        }
        public ProductArticle(ProductModel model, int size, string color)
        {
            Id = Guid.NewGuid();
            Model = model;
            Size = size;
            Color = color;
        }
    }
}
