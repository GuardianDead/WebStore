using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Индификатор")]
        public override int Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public override string Name { get; set; }

        public Role()
        {
        }
        public Role(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
