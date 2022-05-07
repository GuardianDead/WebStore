using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Category
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        [JsonIgnore]
        [Display(AutoGenerateField = false)]
        [DisplayName("Подкатегории")]
        public List<Subcategory> Subcategories { get; }

        public Category()
        {
        }
        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
