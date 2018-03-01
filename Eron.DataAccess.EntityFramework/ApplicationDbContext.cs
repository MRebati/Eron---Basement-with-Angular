using System.Data.Entity;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Entities.User;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eron.DataAccess.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region base

        public DbSet<EronFile> EronFiles { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<BannerSlider> BannerSliders { get; set; }

        public DbSet<UserMessage> UserMessages { get; set; }

        #endregion

        #region financial

        #region Base

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<InvoiceLog> InvoiceLogs { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<FinanceTransaction> FinanceTransactions { get; set; }
        public DbSet<ServiceReview> Reviews { get; set; }

        #endregion

        #region Order

        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<TariffItem> TariffItems { get; set; }
        public DbSet<TariffPrice> TariffPrices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }

        #endregion

        #region Shop

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<ProductPropertyName> ProductPropertyNames { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        #endregion

        #endregion
    }
}
