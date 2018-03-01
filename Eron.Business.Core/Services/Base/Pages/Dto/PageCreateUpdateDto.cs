using System.ComponentModel.DataAnnotations;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.Entities.Base;

namespace Eron.Business.Core.Services.Base.Pages.Dto
{
    [MapsTo(typeof(Page))]
    [MapsFrom(typeof(Page))]
    public class PageCreateUpdateDto : EntityEntryDto<int>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string CreatorUserId { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }
    }
}