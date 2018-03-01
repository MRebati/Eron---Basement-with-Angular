using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum TariffItemType
    {
        [DisplayName("رنگ",0)]
        Color = 0,

        [DisplayName("سایز",1)]
        Size = 1,

        [DisplayName("نوع",2)]
        Type = 2,

        [DisplayName("فی",3)]
        Quantity = 3,

        [DisplayName("کیفیت",4)]
        Quality = 4,

        //[DisplayName("نام فایل ارسالی",5)]
        //FileName = 5
    }
}