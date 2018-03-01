using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using AutoMapper;
using Eron.Business.Core.Core;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.User;
using Eron.Core.Exceptions;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Eron.Business.Core.Services.Base.Authentication
{
    public class UserAppService : ManagementSystemService, IUserAppService
    {
        #region Ctor
        public UserAppService(
            IManagementUnitOfWork unitOfWork,
            TenantType tenantType = TenantType.WebService
        ) : base(unitOfWork, tenantType)
        {
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        #endregion

        #region Query

        public async Task<ApplicationUserDto> GetUserInformation(string id)
        {
            var result = await UserManager.FindByIdAsync(id);
            if (result == null)
                result = await UserManager.FindByEmailAsync(id);
            return result.MapTo<ApplicationUserDto>();
        }

        public async Task<List<ApplicationUserDto>> GetAll()
        {
            var result = await UserManager.Users.ToListAsync();
            return result.MapTo<List<ApplicationUserDto>>();
        }

        public async Task<bool> CurrentUserInfoIsComplete()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = await GetUserInformation(userId);

            return
                user.Address != null &&
                user.FirstName != null &&
                user.LastName != null &&
                user.PostalCode != null;
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            var roles = await UnitOfWork.AppContext.Roles.ToListAsync();
            return roles;
        }

        public Task<ApplicationUserDto> CurrentUserInfo()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return GetUserInformation(userId);
        }


        public async Task<List<IdentityRole>> GetUserRoles(string userId)
        {
            var userRoles = (await UnitOfWork.AppContext.Users.FirstOrDefaultAsync(x => x.Id == userId))?.Roles.Select(x => x.RoleId).ToList();
            var roles = await UnitOfWork.AppContext.Roles.Where(x => userRoles.Contains(x.Id)).ToListAsync();
            return roles;
        }

        #endregion

        #region Command



        public async Task<IdentityResult> Create(ApplicationUserCreateDto input)
        {
            var userProxy = input.MapTo<ApplicationUserProxyDto>();
            var user = userProxy.MapTo<ApplicationUser>();
            user.UserName = user.Email;
            user.EmailConfirmed = true;

            var result = await UserManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                if (input.SelectedRoles != null)
                {
                    var roleResult = await UserManager.AddToRolesAsync(user.Id, input.SelectedRoles);
                    if (!result.Succeeded)
                    {
                        return roleResult;
                    }
                }

                return result;
            }
            return result;
        }

        public async Task<bool> Update(ApplicationUserUpdateDto input)
        {
            if (!input.IsUpdateEntry())
                throw new EntityNotFoundException();

            var user = await UserManager.FindByIdAsync(input.Id);
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            var userOriginal = UnitOfWork.AppContext.Users.Find(user.Id);
            if (user == null)
                throw new EntityNotFoundException();

            var updateUser = input.MapTo<ApplicationUser>();

            updateUser.Email = user.Email;
            updateUser.UserName = user.Email;
            updateUser.PasswordHash = userOriginal.PasswordHash;
            updateUser.SecurityStamp = userOriginal.SecurityStamp;

            Mapper.Map(updateUser, user);
            await UserManager.UpdateAsync(user);

            //input.SelectedRoles = input.SelectedRoles ?? new string[] { };

            var result = await UserManager.AddToRolesAsync(user.Id, userRoles.ToArray<string>());

            if (!result.Succeeded)
            {
                return false;
            }

            //result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(input.SelectedRoles).ToArray<string>());

            //if (!result.Succeeded)
            //{
            //    return false;
            //}
            return true;
        }

        public async Task<IdentityResult> UpdateAsAdmin(ApplicationUserUpdateDto input)
        {
            if (!input.IsUpdateEntry())
                throw new EntityNotFoundException();

            var user = await UserManager.FindByIdAsync(input.Id);
            if (user == null)
                throw new EntityNotFoundException();

            var updateUser = input.MapTo<ApplicationUser>();
            updateUser.UserName = input.Email;
            await UserManager.UpdateAsync(updateUser);

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            input.SelectedRoles = input.SelectedRoles ?? new string[] { };

            var result = await UserManager.AddToRolesAsync(user.Id, input.SelectedRoles.Except(userRoles).ToArray<string>());

            if (!result.Succeeded)
            {
                return result;
            }
            result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(input.SelectedRoles).ToArray<string>());
            return result;
        }

        public async Task<IdentityResult> Delete(string id)
        {
            if (id == null)
            {
                throw new EntityNotFoundException();
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            var roles = (await UserManager.GetRolesAsync(user.Id));
            var result = await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());
            if (result != null && result.Succeeded)
            {
                result = await UserManager.DeleteAsync(user);
            }
            return result;
        }

        #region Helpers

        #endregion Helpers

        #endregion
    }
}
