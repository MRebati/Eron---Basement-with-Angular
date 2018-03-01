using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eron.Core.Entities.Base.ControlPanel.Navigation
{
    public class NavigationModel
    {
        public List<NavigationModel> NavigationModels { get; set; }

        public string Url { get; set; }

        public string ComponentName { get; set; }
    }
}
