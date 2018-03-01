using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eron.Core.ValueObjects
{
    public class SelectListObject
    {
        public SelectListObject(object id, string displayName, bool isSelected = false)
        {
            this.DisplayName = displayName;
            this.Id = id.ToString();
            this.IsSelected = isSelected;
        }

        public string Id { get; set; }

        public string DisplayName { get; set; }

        public bool IsSelected { get; set; }
    }
}
