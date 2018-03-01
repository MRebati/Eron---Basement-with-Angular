using System.Collections.Generic;
using System.Linq;

namespace Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto
{
    public class ProductCategoryReOrderDto
    {
        public int Id { get; set; }

        public List<ProductCategoryReOrderDto> Children { get; set; }

        public bool HasChildren => Children!= null && Children.Any();
    }
}