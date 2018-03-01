using System.Collections.Generic;
using System.Linq;

namespace Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto
{
    public class TariffCategoryReOrderDto
    {
        public int Id { get; set; }

        public List<TariffCategoryReOrderDto> Children { get; set; }

        public bool HasChildren => Children != null && Children.Any();
    }
}