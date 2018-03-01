using System.Collections.Generic;
using System.Linq;

namespace Eron.Business.Core.Services.Base.Navigation.Dto
{
    public class LinkReOrderDto
    {
        public int Id { get; set; }

        public List<LinkReOrderDto> Children { get; set; }

        public bool HasChildren => Children != null && Children.Any();
    }
}