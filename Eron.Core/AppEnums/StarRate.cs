using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum StarRate
    {
        [DisplayName("خیلی بد", 1)]
        VeryBad = 1,
        [DisplayName("بد", 2)]
        Bad = 2,
        [DisplayName("خوب", 3)]
        Good = 3,
        [DisplayName("بسیار خوب", 4)]
        VeryGood = 4,
        [DisplayName("عالی", 5)]
        Excellent = 5
    }
}