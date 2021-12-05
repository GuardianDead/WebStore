using Innofactor.EfCoreJsonValueConverter;
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
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Гарантия в днях")]
        public int Guarantee { get; set; }
        [Required]
        [DisplayName("Страна производитель")]
        public string СountryManufacturer { get; set; }
        [Required]
        [DisplayName("Пол")]
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
        public IEnumerable<string> Materials { get; set; }
        [Required]
        [JsonField]
        [DisplayName("Фотографии")]
        public IEnumerable<byte[]> Photos { get; set; }
        [Required]
        [DisplayName("Время создания")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreation { get; set; }

        public ProductModel()
        {
        }
        public ProductModel(string name, decimal price, int guarantee, string countryManufacturer,
            UserGenderType userGenderType, string brand, Subcategory productSubcategory, byte[] mainPhoto,
            Dictionary<string, string> features, IEnumerable<string> materials, IEnumerable<byte[]> photos,
            DateTime dateTimeCreation)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Guarantee = guarantee;
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
