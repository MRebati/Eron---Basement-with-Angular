using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.EntityFramework.Infrastructure;
using Eron.DataAccess.EntityFramework.Repositories.Base;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Order;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Shop;
using Ninject.Modules;

namespace Eron.SharedKernel.DependancyResolver.DataAccessModules
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            #region Base

            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind(typeof(IReadOnlyRepository<>)).To(typeof(ReadOnlyRepository<>));

            this.Bind<ILinkRepository>().To<LinkRepository>();
            this.Bind<IPageRepository>().To<PageRepository>();
            this.Bind<ITenantRepository>().To<TenantRepository>();
            this.Bind<IEronFileRepository>().To<EronFileRepository>();
            this.Bind<IBannerSliderRepository>().To<BannerSliderRepository>();            
            this.Bind<IUserMessageRepository>().To<UserMessageRepository>();

            #endregion

            #region Financial

            #region Base

            this.Bind<IInvoiceRepository>().To<InvoiceRepository>();
            this.Bind<IInvoiceItemRepository>().To<InvoiceItemRepository>();
            this.Bind<IOfferRepository>().To<OfferRepository>();
            this.Bind<ICartRepository>().To<CartRepository>();
            this.Bind<IWishListRepository>().To<WishListRepository>();
            this.Bind<IInvoiceLogRepository>().To<InvoiceLogRepository>();
            this.Bind<IServiceReviewRepository>().To<ServiceReviewRepository>();
            this.Bind<IFinanceTransactionRepository>().To<FinanceTransactionRepository>();

            #endregion

            #region Order

            this.Bind<IOrderRepository>().To<OrderRepository>();
            this.Bind<ITariffRepository>().To<TariffRepository>();
            this.Bind<ITariffItemRepository>().To<TariffItemRepository>();
            this.Bind<ITariffPriceRepository>().To<TariffPriceRepository>();
            this.Bind<ITariffCategoryRepository>().To<TariffCategoryRepository>();

            #endregion

            #region Shop
            
            this.Bind<IProductRepository>().To<ProductRepository>();
            this.Bind<IProductPriceRepository>().To<ProductPriceRepository>();
            this.Bind<IProductPropertyRepository>().To<ProductPropertyRepository>();
            this.Bind<IProductPropertyNameRepository>().To<ProductPropertyNameRepository>();
            this.Bind<IProductCategoryRepository>().To<ProductCategoryRepository>();

            #endregion

            #endregion
        }
    }
}
