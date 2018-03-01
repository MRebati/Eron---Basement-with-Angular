using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Eron.Business.Core.Services.Base.Pages.Dto;
using Eron.Business.Core.Services.Financial.Base.Invoice.Dto;
using Eron.Business.Core.Services.Financial.Order.Order.Dto;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;

namespace Eron.Business.Core.Services.Base.Search.Dto
{
    public class SearchResponseDto: CommonDto
    {
        public List<ProductCategoryDto> ProductCategories { get; set; }

        public List<ProductDto> Products { get; set; }

        public List<TariffCategoryDto> TariffCategories { get; set; }

        public List<TariffDto> Tariffs { get; set; }

        public List<PageDto> Pages { get; set; }
    }

    public class FullSearchResponseDto: SearchResponseDto
    {
        public List<ApplicationUserDto> Users { get; set; }

        public List<InvoiceDto> Invoices { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}
