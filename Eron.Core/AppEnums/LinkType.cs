using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum LinkType
    {
        [DisplayName("لینک شبکه اجتماعی", 0)]
        SocialMediaLink = 0,

        [DisplayName("لینک داخلی", 1)]
        InternalPage = 1,

        [DisplayName("لینک خارجی", 2)]
        ExternalPage = 2,

        [DisplayName("لینک شماره تماس", 3)]
        Tel = 3,

        [DisplayName("لینک ایمیل", 4)]
        Email = 4
    }
}