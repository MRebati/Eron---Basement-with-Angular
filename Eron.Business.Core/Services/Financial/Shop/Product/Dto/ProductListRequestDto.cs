using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Shop.Product.Dto
{
    public class ProductListRequestDto: PagedListRequest<Eron.Core.Entities.Financial.Shop.Product>
    {
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public bool? Availability { get; set; }
    }
}