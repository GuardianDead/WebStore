using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductSubcategory
    {
        [Key]
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual ProductCategory Category { get; set; }

        public ProductSubcategory()
        {
        }
        public ProductSubcategory(string name, ProductCategory category)
        {
            Name = name;
            Category = category;
        }
    }
}
