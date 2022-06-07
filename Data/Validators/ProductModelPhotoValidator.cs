using FluentValidation;
using WebStore.Data.Entities;

namespace WebStore.Data.Validators
{
    public class ProductModelPhotoValidator : AbstractValidator<ProductModelPhoto>
    {
        public ProductModelPhotoValidator()
        {
        }
    }
}
