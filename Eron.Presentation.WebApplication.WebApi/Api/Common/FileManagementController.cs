using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.ManagementSettings;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Eron.SharedKernel.Helpers.FileHelper;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Presentation.WebApplication.WebApi.Api.Common
{
    public class FileManagementController : BaseApiController
    {
        protected IFileHelper FileHelper;

        public FileManagementController(IFileHelper fileHelper)
        {
            this.FileHelper = fileHelper;
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        public async Task<IHttpActionResult> Post()
        {
            var files = HttpContext.Current.Request.Files;

            var filesList = new List<EronFile>();

            foreach (string fileName in files)
            {
                if (HttpContext.Current.Request.Files[fileName] != null)
                {
                    HttpPostedFileBase file = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[fileName]);
                    var result = await FileHelper.SaveFileAsync(file.MapTo<HttpPostedFileBase>(), ApplicationFolder.ApplicationFolderName.ImageUploadFolder);
                    filesList.Add(result);
                }
            }

            return Ok(filesList);
        }

        public IHttpActionResult Delete(string id)
        {
            var fileId = Guid.Parse(id);
            var result = FileHelper.DeleteFile(fileId);
            return Ok(result);
        }
    }
}