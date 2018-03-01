using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum OrderType
    {
        [DisplayName("چاپ", 0)]
        Print = 0,

        [DisplayName("کپی", 1)]
        Copy = 1,

        [DisplayName("طراحی", 2)]
        Design = 2,

        [DisplayName("برش لیزر", 3)]
        LazerCut = 3
    }
}