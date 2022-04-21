﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class Category
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        public Category()
        {
        }
        public Category(string name)
        {
            Name = name;
        }
    }
}
