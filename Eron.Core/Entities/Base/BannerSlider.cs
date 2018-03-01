using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Base
{
    public class BannerSlider: Entity<int>
    {
        [Index]
        [StringLength(50)]
        public string GroupName { get; set; }

        public Guid FileId { get; set; }

        public EronFile File { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LinkUrl { get; set; }

        public bool IsSlider { get; set; }
    }
}
