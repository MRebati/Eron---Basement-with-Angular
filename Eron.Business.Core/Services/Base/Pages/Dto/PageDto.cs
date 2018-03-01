using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.Entities.Base;

namespace Eron.Business.Core.Services.Base.Pages.Dto
{
    [MapsTo(typeof(Page))]
    [MapsFrom(typeof(Page))]
    public class PageDto: EntityDto<int>
    {
        public string Title { get; set; }

        [Index(IsUnique = true)]
        public string Slug { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public long Views { get; set; }
    }
}
