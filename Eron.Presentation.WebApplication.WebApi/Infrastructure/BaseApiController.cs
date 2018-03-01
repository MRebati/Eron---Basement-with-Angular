using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Eron.SharedKernel.Helpers.FileHelper;
using Microsoft.AspNet.Identity;

namespace Eron.Presentation.WebApplication.WebApi.Infrastructure
{
    public class BaseApiController : ApiController
    {
        protected string CurrentUserId;
        protected string CurrentUserName;

        public BaseApiController()
        {
            CurrentUserId = User.Identity.GetUserId();
            CurrentUserName = User.Identity.GetUserName();
        }

        protected new OkNegotiatedContentResult<EronWebApiResponse<T>> Ok<T>(T obj)
        {
            if (typeof(T) == typeof(EronWebApiResponse<>)) return Ok(obj);
            var response = new EronWebApiResponse<T>()
            {
                Data = obj
            };
            return base.Ok(response);
        }
    }
}