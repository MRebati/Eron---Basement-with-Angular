using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.AppEnums;
using System.Configuration;

namespace Eron.Core.ManagementSettings
{
    public static class ApplicationSettings
    {
        public static TimeSpan TransactionTimeout = new TimeSpan(0, 5, 0);
        public static MapperType MapperType = MapperType.AutoMapper;
        public static Language DefaultLanguage= Language.Persian;

        public const string Domain = "chaptin.com";
        public const string DefaultAdminUsername = "admin";
        public const string ForgetPasswordEmail = "no-reply@chaptin.com";
        
        public const string DefaultAdminPassword = "alpha007";

        public const string DefaultHeroChars = "CH";

        public const string DefaultKey = "3a4f836a-7603-4009-b2b1-e7b88e94cd13";

        public static class Pagination
        {
            public const int PageSize = 20;
        }

        public static class FinancialSettings
        {
            public const int MaximumInvoiceProgressSteps = 6;
            public const int MaximumOrderProgressSteps = 6;
        }
    }
}
