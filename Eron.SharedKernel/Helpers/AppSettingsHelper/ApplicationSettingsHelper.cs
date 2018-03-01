using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eron.SharedKernel.Helpers.AppSettingsHelper
{
    public static class ApplicationSettingsHelper
    {
        public static T AppSetting<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }

        public static string AppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
