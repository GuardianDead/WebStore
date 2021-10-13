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
        [Required]
        [DisplayName("Количество")]
        public int Count { get; set; }

        public ProductArticle()
        {
        }
        public ProductArticle(ProductModel productModel, int size, string color, int count)
        {
            Id = Guid.NewGuid();
            Model = productModel;
            Size = size;
            Color = color;
            Count = count;
        }
    }
}
