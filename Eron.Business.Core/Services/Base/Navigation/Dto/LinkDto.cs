using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Base.Navigation.Dto
{
    [MapsFrom(typeof(Link))]
    [MapsTo(typeof(Link))]
    public class LinkDto : EntityDto<int>
    {
        public LinkType LinkType { get; set; }

        public LinkPlacement LinkPlacement { get; set; }

        public string Url { get; set; }

        public string LinkText { get; set; }

        public UrlTargetType Target { get; set; }

        public string Image { get; set; }

        public string IconClass { get; set; }

        public int? Priority { get; set; }

        public int? ParentId { get; set; }

        public List<LinkDto> Children { get; set; }
    }
}