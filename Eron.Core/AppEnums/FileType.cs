using Eron.Core.Infrastructure;

namespace Eron.Core.AppEnums
{
    public enum FileType
    {
        [DisplayName("فایل عکس")]
        Image,

        [DisplayName("فایل فشرده شده")]
        Zip,

        [DisplayName("فایل صدا")]
        Sound,

        [DisplayName("فایل ویدئو")]
        Video,

        [DisplayName("ناشناخته")]
        Unknown
    }
}