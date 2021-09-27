﻿using Innofactor.EfCoreJsonValueConverter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductModel
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Гарантия (дни)")]
        public int Guarantee { get; set; }
        [Required]
        [DisplayName("Страна производитель")]
        public string СountryManufacturer { get; set; }
        [Required]
        [DisplayName("Муж")]
        public bool IsMasculine { get; set; }
        [Required]
        [DisplayName("Бренд")]
        public string Brand { get; set; }
        [Required]
        [DisplayName("Подкатегория")]
        public virtual ProductSubcategory Subcategory { get; set; }
        [Required]
        [DisplayName("Главная фотография")]
        public byte[] MainPhoto { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Характеристики")]
        public Dictionary<string, string> Features { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Материалы")]
        public List<string> Materials { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Фотографии")]
        public List<byte[]> Photos { get; set; }

        public ProductModel()
        {
        }
        public ProductModel(string name, decimal price, int guarantee, string countryManufacturer,
            bool isMasculine, string brand, ProductSubcategory productSubcategory, byte[] mainPhoto,
            Dictionary<string,string> features, List<string> materials, List<byte[]> photos)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Guarantee = guarantee;
            СountryManufacturer = countryManufacturer;
            IsMasculine = isMasculine;
            Brand = brand;
            Subcategory = productSubcategory;
            MainPhoto = mainPhoto;
            Features = features;
            Materials = materials;
            Photos = photos;
        }
    }
}
