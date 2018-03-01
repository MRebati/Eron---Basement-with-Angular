using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Common.Dto
{
    public class EronFileDto: EntityDto<Guid>
    {
        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public DateTime UploadDateTime { get; set; }

        public bool Deleted { get; set; }
    }
}
