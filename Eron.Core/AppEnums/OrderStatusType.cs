using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum OrderStatusType
    {
        [DisplayName("در حال بررسی", 0)]
        Received = 0,

        [DisplayName("در انتظار پرداخت", 1)]
        WaitingForPayment = 1,

        [DisplayName("در حال انجام", 2)]
        Processing = 2,

        [DisplayName("در حال ارسال", 3)]
        Posting = 3,

        [DisplayName("ارسال شده", 4)]
        Posted = 4,

        [DisplayName("تحویل داده شده", 5)]
        Delivered =5,

        [DisplayName("لغو شده", 6)]
        Canceled = 6,

        [DisplayName("سفارش رد شده", 7)]
        CaneledByAdmin = 7,

        [DisplayName("نیاز به صدور فاکتور", 8)]
        NeedInvoice = 8,
    }
}