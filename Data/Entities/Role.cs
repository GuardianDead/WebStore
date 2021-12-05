using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public override int Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public override string Name { get; set; }

        public Role()
        {
        }
        public Role(string name)
        {
            this.Name = name;
        }
    }
}
