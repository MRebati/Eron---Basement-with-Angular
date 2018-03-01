using System;
using System.Data.Entity;
using Eron.Core.Entities.User;
using Eron.DataAccess.Contract.Repositories;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.DataAccess.EntityFramework.Infrastructure;
using Eron.DataAccess.EntityFramework.Repositories.Base;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Order;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Shop;
using Microsoft.AspNet.Identity.EntityFramework;
using TenantRepository = Eron.DataAccess.EntityFramework.Repositories.TenantRepository;

namespace Eron.DataAccess.EntityFramework.UnitOfWorks
{
    public class ManagementUnitOfWork : UnitOfWork, IManagementUnitOfWork
    {
        public ManagementUnitOfWork(
            DbContext context, 
            IdentityDbContext<ApplicationUser> appContext,
            ILinkRepository linkRepository, 
            IPageRepository pageRepository, 
            ITenantRepository tenantRepository, 
            IEronFileRepository fileRepository, 
            IBannerSliderRepository bannerSliderRepository, 
            IOfferRepository offerRepository, 
            IInvoiceRepository invoiceRepository, 
            IInvoiceItemRepository invoiceItemRepository,
            IInvoiceLogRepository invoiceLogRepository,
            IWishListRepository wishListRepository, 
            ICartRepository cartRepository, 
            IOrderRepository orderRepository, 
            ITariffRepository tariffRepository, 
            ITariffPriceRepository tariffPriceRepository, 
            ITariffItemRepository tariffItemRepository, 
            IProductRepository productRepository, 
            IProductPriceRepository productPriceRepository, 
            IProductPropertyRepository productPropertyRepository, 
            IProductPropertyNameRepository productPropertyNameRepository, 
            IProductCategoryRepository productCategoryRepository, 
            IServiceReviewRepository serviceReviewRepository, 
            ITariffCategoryRepository tariffCategoryRepository, IFinanceTransactionRepository financeTransactionRepository) : base(context)
        {
            AppContext = appContext;
            LinkRepository = linkRepository;
            PageRepository = pageRepository;
            TenantRepository = tenantRepository;
            FileRepository = fileRepository;
            BannerSliderRepository = bannerSliderRepository;
            OfferRepository = offerRepository;
            InvoiceRepository = invoiceRepository;
            InvoiceItemRepository = invoiceItemRepository;
            InvoiceLogRepository = invoiceLogRepository;
            WishListRepository = wishListRepository;
            CartRepository = cartRepository;
            OrderRepository = orderRepository;
            TariffRepository = tariffRepository;
            TariffPriceRepository = tariffPriceRepository;
            TariffItemRepository = tariffItemRepository;
            ProductRepository = productRepository;
            ProductPriceRepository = productPriceRepository;
            ProductPropertyRepository = productPropertyRepository;
            ProductPropertyNameRepository = productPropertyNameRepository;
            ProductCategoryRepository = productCategoryRepository;
            ServiceReviewRepository = serviceReviewRepository;
            TariffCategoryRepository = tariffCategoryRepository;
            FinanceTransactionRepository = financeTransactionRepository;
        }

        public IdentityDbContext<ApplicationUser> AppContext { get; }
        public ILinkRepository LinkRepository { get; }
        public IPageRepository PageRepository { get; }
        public ITenantRepository TenantRepository { get; }
        public IEronFileRepository FileRepository { get; }
        public IBannerSliderRepository BannerSliderRepository { get; }
        public IOfferRepository OfferRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IInvoiceItemRepository InvoiceItemRepository { get; }
        public IInvoiceLogRepository InvoiceLogRepository { get; set; }
        public IWishListRepository WishListRepository { get; }
        public ICartRepository CartRepository { get; }
        public IServiceReviewRepository ServiceReviewRepository { get; }
        public IFinanceTransactionRepository FinanceTransactionRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public ITariffRepository TariffRepository { get; }
        public ITariffPriceRepository TariffPriceRepository { get; }
        public ITariffItemRepository TariffItemRepository { get; }
        public ITariffCategoryRepository TariffCategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductPriceRepository ProductPriceRepository { get; }
        public IProductPropertyRepository ProductPropertyRepository { get; }
        public IProductPropertyNameRepository ProductPropertyNameRepository { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; }
    }
}
