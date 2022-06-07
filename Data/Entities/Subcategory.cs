using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Subcategory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Категория")]
        [Display(AutoGenerateField = false)]
        public int CategoryId { get; set; }
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

        public override string ToString() => Name;
    }
}
