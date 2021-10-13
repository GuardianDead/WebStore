﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class CartProduct
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Товар")]
        public ProductArticle ProductArticle { get; set; }
        [Required]
        [DisplayName("Количество")]
        public int Count { get; set; }

        public CartProduct()
        {
        }
        public CartProduct(ProductArticle productArticle, int count)
        {
            ProductArticle = productArticle;
            Count = count;
        }
    }
}