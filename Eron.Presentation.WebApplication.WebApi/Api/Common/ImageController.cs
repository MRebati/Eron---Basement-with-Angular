using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Eron.SharedKernel.Helpers.StringExtensions;

namespace Eron.Presentation.WebApplication.WebApi.Api.Common
{
    public class ImageController : BaseApiController
    {
        private IDefaultUnitOfWork _unitOfWork;

        public ImageController(IDefaultUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HttpResponseMessage Get(string id)
        {
            if (id.IsNullOrWhiteSpace())
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var fileId = Guid.Parse(id);
            var fileEntity = _unitOfWork.FileRepository.GetById(fileId);

            if(fileEntity.Deleted)
                return new HttpResponseMessage(HttpStatusCode.NoContent);

            if (fileEntity.FileData != null)
            {
                using (MemoryStream ms = new MemoryStream(fileEntity.FileData))
                {
                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(ms.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                    return result;
                }

            }
            else if (fileEntity.FileUrl.IsPopulated())
            {
                var fileAddress = HttpContext.Current.Server.MapPath(fileEntity.FileUrl);
                var file = File.ReadAllBytes(fileAddress);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(file);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}