using Eron.Core.Entities.User;
using Eron.Core.ManagementSettings;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Eron.DataAccess.EntityFramework.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Eron.DataAccess.EntityFramework.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == ApplicationSettings.DefaultAdminUsername))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = ApplicationSettings.DefaultAdminUsername,
                    Email = ApplicationSettings.DefaultAdminUsername + "@" + ApplicationSettings.Domain,
                    EmailConfirmed = true
                };

                manager.Create(user, ApplicationSettings.DefaultAdminPassword);
                manager.AddToRole(user.Id, "admin");
            }
        }
    }
}
