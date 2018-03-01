using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Base.BannerSlider.Dto
{
    [MapsTo(typeof(Eron.Core.Entities.Base.BannerSlider))]
    [MapsFrom(typeof(Eron.Core.Entities.Base.BannerSlider))]
    public class BannerSliderDto: EntityDto<int>
    {
        public string GroupName { get; set; }

        public Guid FileId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LinkUrl { get; set; }
    }
}
