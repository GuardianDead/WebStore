using Innofactor.EfCoreJsonValueConverter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class ProductModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public string Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Цена")]
        public int Price { get; set; }
        [Required]
        [DisplayName("Гарантия (в днях)")]
        public int DaysGuarantee { get; set; }
        [Required]
        [DisplayName("Страна производитель")]
        public string СountryManufacturer { get; set; }
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
        [DisplayName("Дата создания")]
        public DateTime DateTimeCreation { get; set; }

        [DisplayName("Подкатегория")]
        [Display(AutoGenerateField = false)]
        public int SubcategoryId { get; set; }
        [Required]
        [DisplayName("Подкатегория")]
        public Subcategory Subcategory { get; set; }

        [JsonIgnore]
        [Display(AutoGenerateField = false)]
        [DisplayName("Подкатегория")]
        public List<ProductArticle> ProductArticles { get; }

        public ProductModel()
        {
        }
        public ProductModel(string name, int price, int daysGuarantee, string countryManufacturer, string brand, Subcategory subcategory, byte[] mainPhoto, Dictionary<string, string> features, List<string> materials, List<byte[]> photos, DateTime dateTimeCreation)
        {
            Id = Guid.NewGuid().ToString("N");
            Name = name;
            Price = price;
            DaysGuarantee = daysGuarantee;
            СountryManufacturer = countryManufacturer;
            Brand = brand;
            Subcategory = subcategory;
            MainPhoto = mainPhoto;
            Features = features;
            Materials = materials;
            Photos = photos;
            DateTimeCreation = dateTimeCreation;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
