using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Data.Entities
{
    public class CartProduct
    {
        [Key]
        [Required]
        [DisplayName("Номер")]
        public int Id { get; init; }
        [Required]
        [DisplayName("Артикул")]
        public ProductArticle Article { get; set; }
        [Required]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [Required]
        [DisplayName("Товар выбран?")]
        public bool IsSelected { get; set; }

        public CartProduct()
        {
        }
        public CartProduct(ProductArticle article, int count)
        {
            Article = article;
            Count = count;
        }
    }
}
