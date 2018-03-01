using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum DatePeriodType
    {
        [DisplayName("امروز", 0)]
        Day = 0,

        [DisplayName("هفته", 1)]
        Week = 1,

        [DisplayName("ماه", 2)]
        Month = 2,

        [DisplayName("سال",3)]
        Year = 3,

        [DisplayName("همه", 4)]
        All = 4
    }
}