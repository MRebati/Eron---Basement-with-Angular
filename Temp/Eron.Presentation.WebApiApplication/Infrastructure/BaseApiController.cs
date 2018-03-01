using System.Web.Http;
using Eron.SharedKernel.Helpers.AppSettingsHelper;

namespace Eron.Presentation.WebApiApplication.Infrastructure
{
    public class InstagramBaseApiController : ApiController
    {
        protected string AppSettings(string key)
        {
            return ApplicationSettingsHelper.AppSetting(key);
        }
        protected T AppSettings<T>(string key)
        {
            return ApplicationSettingsHelper.AppSetting<T>(key);
        }

        public InstagramBaseApiController()
        {
        }
    }
}