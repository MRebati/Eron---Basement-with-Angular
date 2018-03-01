using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Eron.Presentation.WebApplication.WebApi.Providers
{
    public class UserValidationProvider<TUser> : IIdentityValidator<TUser>
        where TUser : class, Microsoft.AspNet.Identity.IUser
    {
        private readonly UserManager<TUser> _userManager;

        public UserValidationProvider(UserManager<TUser> manager)
        {
            _userManager = manager;
        }

        public async Task<IdentityResult> ValidateAsync(TUser user)
        {
            var errors = new List<string>();

            if (_userManager != null)
            {
                //check username availability. and add a custom error message to the returned errors list.
                var existingAccount = await _userManager.FindByNameAsync(user.UserName);
                if (existingAccount != null && existingAccount.Id != user.Id)
                    errors.Add("نام کاربری انتخابی قبلا انتخاب شده و قابل تخصیص نمی باشد. از نام کاربری دیگری استفاده کنید");
            }

            //set the returned result (pass/fail) which can be read via the Identity Result returned from the CreateUserAsync
            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}