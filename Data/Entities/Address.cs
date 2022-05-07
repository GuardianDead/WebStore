using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Address
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Страна")]
        public string Country { get; set; }
        [Required]
        [DisplayName("Область/регион")]
        public string Region { get; set; }
        [Required]
        [DisplayName("Город")]
        public string City { get; set; }
        [Required]
        [DisplayName("Улица")]
        public string Street { get; set; }
        [Required]
        [DisplayName("Почтовый индекс")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public Address()
        {
        }
        public Address(string country, string region, string city, string street, string postalCode)
        {
            Country = country;
            City = city;
            Region = region;
            Street = street;
            PostalCode = postalCode;
        }

        public override string ToString()
        {
            return $"{Id} - {string.Join('-', Country, Region, City, Street, PostalCode)}";
        }
    }
}
