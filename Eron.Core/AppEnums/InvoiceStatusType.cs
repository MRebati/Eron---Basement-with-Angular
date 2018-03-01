using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum InvoiceStatusType
    {


        [DisplayName("در انتظار پرداخت", 0)]
        WaitingForPayment = 0,

        [DisplayName("در حال بررسی", 1)]
        Received = 1,

        [DisplayName("در حال بسته بندی", 2)]
        Processing = 2,

        [DisplayName("در حال ارسال", 3)]
        Posting = 3,

        [DisplayName("ارسال شده", 4)]
        Posted = 4,

        [DisplayName("تحویل داده شده", 5)]
        Delivered = 5,

        [DisplayName("لغو شده", 6)]
        Canceled = 6,

        [DisplayName("منقضی شده", 7)]
        Expired = 7,
    }

    public enum InvoiceType
    {
        [DisplayName("سفارش",0)]
        Order = 0,

        [DisplayName("فروش محصول", 1)]
        Cart = 1
    }
}