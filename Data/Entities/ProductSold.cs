using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class ProductSold
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Жизнь товара в бд (дни)")]
        public int LifeTime { get; }
        [Required]
        [DisplayName("Заказ")]
        public virtual Order Order { get; set; }

        public ProductSold()
        {
        }
        public ProductSold(Guid id, Order order, int lifeTime)
        {
            Id = id;
            Order = order;
            LifeTime = lifeTime;
        }
    }
}
