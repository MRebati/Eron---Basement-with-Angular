using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Entities.Base;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Order
{
    public class TariffCategory : Entity<int>
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        [Required]
        public string Slug { get; set; }

        public bool Promoted { get; set; }

        public bool ViewOnHomePage { get; set; }

        public Guid? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual EronFile Image { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual TariffCategory Parent { get; set; }

        public ICollection<TariffCategory> Children { get; set; }

        public ICollection<Tariff> Tariffs { get; set; }
    }
}