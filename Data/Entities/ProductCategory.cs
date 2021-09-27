using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductCategory
    {
        [Key]
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        public ProductCategory()
        {
        }
        public ProductCategory(string name)
        {
            Name = name;
        }
    }
}
