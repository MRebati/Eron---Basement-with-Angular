using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eron.Business.Core.Services.Base.Authentication
{
    public interface IUserAppService : IApplicationService
    {

        #region Query

        Task<ApplicationUserDto> GetUserInformation(string id);

        Task<List<ApplicationUserDto>> GetAll();

        Task<bool> CurrentUserInfoIsComplete();

        Task<ApplicationUserDto> CurrentUserInfo();

        Task<List<IdentityRole>> GetAllRoles();

        Task<List<IdentityRole>> GetUserRoles(string userId);

        #endregion

        #region Command

        Task<IdentityResult> Create(ApplicationUserCreateDto input);

        Task<bool> Update(ApplicationUserUpdateDto input);

        Task<IdentityResult> UpdateAsAdmin(ApplicationUserUpdateDto input);

        Task<IdentityResult> Delete(string id);

        #endregion
    }
}