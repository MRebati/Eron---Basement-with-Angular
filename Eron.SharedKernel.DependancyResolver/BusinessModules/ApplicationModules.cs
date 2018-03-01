using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication;
using Eron.Business.Core.Services.Base.BannerSlider;
using Eron.Business.Core.Services.Base.Insight;
using Eron.Business.Core.Services.Base.Navigation;
using Eron.Business.Core.Services.Base.Pages;
using Eron.Business.Core.Services.Base.Search.SearchInControlPanel;
using Eron.Business.Core.Services.Base.Search.SearchInWebsite;
using Eron.Business.Core.Services.Common;
using Eron.Business.Core.Services.Financial.Base.Bank;
using Eron.Business.Core.Services.Financial.Base.Cart;
using Eron.Business.Core.Services.Financial.Base.Invoice;
using Eron.Business.Core.Services.Financial.Order.Order;
using Eron.Business.Core.Services.Financial.Order.Tariff;
using Eron.Business.Core.Services.Financial.Order.TariffCategory;
using Eron.Business.Core.Services.Financial.Shop.Product;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory;
using Eron.Business.Core.Services.Financial.Shop.WishList;
using Eron.Business.Core.Services.Financial.Shop.WishList.Dto;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.User;
using Eron.Core.Infrastructure;
using Eron.Core.ValueObjects;
using Eron.DataAccess.EntityFramework;
using Eron.SharedKernel.Helpers.FileHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Web.Common;

namespace Eron.SharedKernel.DependancyResolver.BusinessModules
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            #region Base Modules

            Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            Bind<UserManager<ApplicationUser>>().ToSelf();
            Bind<IOwinContext>().ToSelf();
            Bind<IAuthenticationManager>().ToSelf();

            #endregion Base Modules

            #region Helper Modules

            Bind<IFileHelper>().To<FileHelper>();

            #endregion Helper Modules

            #region Service Modules

            #region Base Services

            Bind<ICommonAppService>().To<CommonAppService>();
            Bind<IUserAppService>().To<UserAppService>();
            Bind<ILinkAppService>().To<LinkAppService>();
            Bind<IPageAppService>().To<PageAppService>();
            Bind<ISearchInWebsiteAppService>().To<SearchInWebsiteAppService>();
            Bind<ISearchInControlPanelAppService>().To<SearchInControlPanelAppService>();
            Bind<IBannerSliderAppService>().To<BannerSliderAppService>();
            Bind<IInsightAppService>().To<InsightAppService>();
            Bind<IWishListAppService>().To<WishListAppService>();

            #endregion Base Services

            #region Financial Services

            Bind<ITariffAppService>().To<TariffAppService>();
            Bind<IProductAppService>().To<ProductAppService>();
            Bind<IProductCategoryAppService>().To<ProductCategoryAppService>();
            Bind<ICartAppService>().To<CartAppService>();
            Bind<ITariffCategoryAppService>().To<TariffCategoryAppService>();
            Bind<IOrderAppService>().To<OrderAppService>();
            Bind<IInvoiceAppService>().To<InvoiceAppService>();
            Bind<IBankAppService>().To<BankAppService>();

            #endregion Financial Services

            #endregion Service Modules

        }
    }
}
