using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class Subcategory
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Категория")]
        public Category Category { get; set; }

        public Subcategory()
        {
        }
        public Subcategory(string name, Category category)
        {
            Name = name;
            Category = category;
        }
    }
}
