using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum TenantType
    {
        [DisplayName("وبسایت")]
        WebApplication,

        [DisplayName("نرم افزار موبایل")]
        MobileApplication,

        [DisplayName("وب سرویس")]
        WebService
    }
}
