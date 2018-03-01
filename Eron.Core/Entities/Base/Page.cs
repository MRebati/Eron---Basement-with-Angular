using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Base
{
    public class Page : Entity<int>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string CreatorUserId { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public long? Views { get; set; }
    }
}