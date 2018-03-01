using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum LinkPlacement
    {
        [DisplayName("منو", 0)]
        Menu = 0,

        [DisplayName("فوتر", 1)]
        Footer = 1,

        [DisplayName("لینک شبکه اجتماعی", 2)]
        SocialMenu = 2,

        [DisplayName("نامعلوم", 3)]
        Unknown = 3
    }
}