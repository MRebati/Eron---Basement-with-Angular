
using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum CustomerType
    {
        [DisplayName("کاربر نهایی", 0)]
        EndUserCustomer = 0,

        //[DisplayName("همکار", 1)]
        //College = 1
    }

    public enum UnitType
    {
        [DisplayName("کیلو", 0)]
        Kilo = 0,
        [DisplayName("تعداد", 1)]
        Number = 1,
        [DisplayName("متر", 2)]
        Meter = 2

    }
}