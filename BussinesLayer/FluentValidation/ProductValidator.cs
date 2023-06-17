using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BussinesLayer.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı boş bırakılamaz.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ürün adı minimum 3");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Boş Olamaz");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(2).WithMessage("2TRY den düşük olamaz");
        }
    }
}
