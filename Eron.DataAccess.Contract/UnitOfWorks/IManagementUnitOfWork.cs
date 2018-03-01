using Eron.Core.Entities.User;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eron.DataAccess.Contract.UnitOfWorks
{
    public interface IManagementUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// This Repository will return CRUD Actions related to ApplcationUsers.
        /// </summary>
        IdentityDbContext<ApplicationUser> AppContext { get; }

        #region Base

        ILinkRepository LinkRepository { get; }

        IPageRepository PageRepository { get; }

        ITenantRepository TenantRepository { get; }

        IEronFileRepository FileRepository { get; }

        IBannerSliderRepository BannerSliderRepository { get; }

        #endregion

        #region Financial

        #region Base

        IOfferRepository OfferRepository { get; }

        IInvoiceRepository InvoiceRepository { get; }

        IInvoiceItemRepository InvoiceItemRepository { get; }

        IInvoiceLogRepository InvoiceLogRepository { get; set; }

        IWishListRepository WishListRepository { get; }

        ICartRepository CartRepository { get; }

        IServiceReviewRepository ServiceReviewRepository { get; }

        IFinanceTransactionRepository FinanceTransactionRepository { get; }

        #endregion

        #region Order

        IOrderRepository OrderRepository { get; }

        ITariffRepository TariffRepository { get; }

        ITariffPriceRepository TariffPriceRepository { get; }

        ITariffItemRepository TariffItemRepository { get; }
        ITariffCategoryRepository TariffCategoryRepository { get; }

        #endregion

        #region Shopping

        IProductRepository ProductRepository { get; }

        IProductPriceRepository ProductPriceRepository { get; }

        IProductPropertyRepository ProductPropertyRepository { get; }

        IProductPropertyNameRepository ProductPropertyNameRepository { get; }

        IProductCategoryRepository ProductCategoryRepository { get; }

        #endregion

        #endregion
    }
}
