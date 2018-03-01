using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eron.Business.Core.Infrastructure;
using Eron.Core.AppEnums;
using Eron.DataAccess.Contract.UnitOfWorks;
using Postal;

namespace Eron.Business.Core.Services.Email
{
    public interface IEmailAppService: IApplicationService
    {
        void SendEmail(string templateAddress, Postal.Email model);
    }

    public class EmailAppService : ManagementSystemService, IEmailAppService
    {
        public EmailAppService(
            IManagementUnitOfWork unitOfWork, 
            TenantType tenantType = TenantType.WebService
        ) : base(unitOfWork, tenantType)
        {
        }

        public void SendEmail(string templateAddress, Postal.Email model)
        {
            // default was:
            //@"..\..\Views"
            var viewsPath = Path.GetFullPath(templateAddress);

            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var service = new EmailService(engines);

            dynamic email = model;
            // Will look for Test.cshtml or Test.vbhtml in Views directory.
            service.Send(email);
        }
    }
}
