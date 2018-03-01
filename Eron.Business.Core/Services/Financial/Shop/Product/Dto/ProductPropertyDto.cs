using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.Entities.Financial.Shop;

namespace Eron.Business.Core.Services.Financial.Shop.Product.Dto
{
    [MapsFrom(typeof(ProductProperty))]
    [MapsTo(typeof(ProductProperty))]
    public class ProductPropertyDto: EntityDto<long>
    {
        [MapsToAndFromProperty(typeof(ProductProperty), "Name")]
        public string PropertyName { get; set; }

        [MapsToAndFromProperty(typeof(ProductProperty), "Value")]
        public string PropertyValue { get; private set; }

        public int ProductPropertyNameId { get; set; }
    }
}