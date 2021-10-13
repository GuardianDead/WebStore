using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class Address
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; private init; }
        [Required]
        [DisplayName("Страна")]
        public string Country { get; set; }
        [Required]
        [DisplayName("Город")]
        public string City { get; set; }
        [Required]
        [DisplayName("Улица")]
        public string Street { get; set; }
        [Required]
        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }
        [Required]
        [DisplayName("Почтовый индекс")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public Address()
        {
        }
        public Address(string country, string city, string street,
            string houseNumber, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
        }
    }
}
