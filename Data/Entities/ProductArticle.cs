﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductArticle
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public string Id { get; init; }
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
            Id = Guid.NewGuid().ToString("N");
            Model = model;
            Size = size;
            Color = color;
        }
    }
}
