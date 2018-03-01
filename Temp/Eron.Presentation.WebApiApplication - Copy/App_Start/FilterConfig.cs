using System.Web;
using System.Web.Mvc;

namespace Eron.Presentation.WebApiApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
