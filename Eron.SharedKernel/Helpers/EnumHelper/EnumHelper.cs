using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.ValueObjects;

namespace Eron.SharedKernel.Helpers.EnumHelper
{
    public class DisplayNameAttribute : Attribute
    {
        public string Name { get; set; }

        public DisplayNameAttribute(string name) : base()
        {
            Name = name;
        }
    }

    public static class EnumHelper
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetDisplayName(this Enum enumVal)
        {
            return enumVal.GetAttributeOfType<DisplayNameAttribute>().Name;
        }

        public static List<SelectListObject> GetByEnumName(string enumName,bool useEnglishName = false)
        {
            var list = new List<SelectListObject>();

            // Get the assembly containing the enum - Here it's the one executing
            var assembly = Assembly.GetExecutingAssembly();

            // Get the enum type
            var enumType = assembly.GetType("Eron.Core.AppEnums."+enumName);

            // Get the enum value
            var enumFields = enumType.GetFields(); //"Bar").GetValue(null);

            foreach (var field in enumFields)
            {
                var enFieldName = field.Name;
                var faFieldName = (field.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute)?.Name;

                var fieldId = field.GetValue(null);

                list.Add(new SelectListObject(fieldId,useEnglishName? enFieldName : faFieldName));
            }

            // Use the enum value
            //Console.WriteLine("{0}|{1}", enumBarValue, (int)enumBarValue);
            
            return list;
        }
    }
}
