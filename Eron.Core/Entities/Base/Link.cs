using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.AppEnums;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Base
{
    public class Link: Entity<int>
    {
        public Link()
        {
            this.Children = new List<Link>();
        }
        public LinkType LinkType { get; set; }

        public LinkPlacement LinkPlacement { get; set; }

        public string Url { get; set; }

        public string LinkText { get; set; }

        public UrlTargetType Target { get; set; }

        public string Image { get; set; }

        public string IconClass { get; set; }

        public int Priority { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Link Parent { get; set; }

        public ICollection<Link> Children { get; set; }
    }
}