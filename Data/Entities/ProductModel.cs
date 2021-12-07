﻿using Innofactor.EfCoreJsonValueConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Types;

namespace WebStore.Data.Entities
{
    public class ProductModel
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public Guid Id { get; private init; }
        [Required]
        [DisplayName("Подкатегория")]
        public Subcategory Subcategory { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Цена")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Гарантия (в днях)")]
        public int DaysGuarantee { get; set; }
        [Required]
        [DisplayName("Страна производитель")]
        public string СountryManufacturer { get; set; }
        [Required]
        [DisplayName("Пол")]
        [EnumDataType(typeof(UserGenderType))]
        [DataType(DataType.Text)]
        public UserGenderType Gender { get; set; }
        [Required]
        [DisplayName("Бренд")]
        public string Brand { get; set; }
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
        [Required]
        [DisplayName("Время создания (в днях)")]
        public DateTime DateTimeCreation { get; set; }

        public ProductModel()
        {
        }
        public ProductModel(string name, decimal price, int daysGuarantee, string countryManufacturer,
            UserGenderType userGenderType, string brand, Subcategory productSubcategory, byte[] mainPhoto,
            Dictionary<string, string> features, List<string> materials, List<byte[]> photos, DateTime dateTimeCreation)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            DaysGuarantee = daysGuarantee;
            СountryManufacturer = countryManufacturer;
            Gender = userGenderType;
            Brand = brand;
            Subcategory = productSubcategory;
            MainPhoto = mainPhoto;
            Features = features;
            Materials = materials;
            Photos = photos;
            DateTimeCreation = dateTimeCreation;
        }
    }
}
