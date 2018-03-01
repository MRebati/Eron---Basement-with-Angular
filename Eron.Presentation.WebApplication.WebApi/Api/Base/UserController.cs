using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Core;
using Eron.Business.Core.Services.Base.Authentication;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Eron.Presentation.WebApplication.WebApi.Api.Base
{
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private IUserAppService _service;
        public UserController(IUserAppService service)
        {
            _service = service;
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = await _service.GetUserInformation(id);
            return Ok(result);
        }

        [Route("userRoles/{userId}")]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> GetUserRoles(string userId)
        {
            var result = await _service.GetUserRoles(userId);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetRoles")]
        public async Task<IHttpActionResult> GetRoles()
        {
            var result = await _service.GetAllRoles();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Post(ApplicationUserCreateDto input)
        {
            var result = await _service.Create(input);
            if (result.Succeeded)
                return Ok(result);
            else
            {
                return GetErrorResult(result);
            }
        }

        [Authorize]
        public async Task<IHttpActionResult> Put(ApplicationUserUpdateDto input)
        {
            input.Id = User.Identity.GetUserId();
            var result = await _service.Update(input);
            return Ok(result);
        }

        [HttpPut]
        [Route("asAdmin")]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> PutAsAdmin(ApplicationUserUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var result = await _service.Delete(id);
            if(!result.Succeeded)
                return GetErrorResult(result);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("checkUserInfo")]
        public async Task<IHttpActionResult> UserHasUnCompleteInfo()
        {
            var result = !(await _service.CurrentUserInfoIsComplete());
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("UserInfo")]
        public async Task<IHttpActionResult> CurrentUserInfo()
        {
            var result = await _service.CurrentUserInfo();
            return Ok(result);
        }

        #region Helpers

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion Helpers
    }
}