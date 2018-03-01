using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Base.BannerSlider.Dto
{
    [MapsTo(typeof(Eron.Core.Entities.Base.BannerSlider))]
    [MapsFrom(typeof(Eron.Core.Entities.Base.BannerSlider))]
    public class BannerSliderCreateOrUpdateDto : EntityEntryDto<int>
    {
        public string GroupName { get; set; }

        public string FileId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LinkUrl { get; set; }
    }
}