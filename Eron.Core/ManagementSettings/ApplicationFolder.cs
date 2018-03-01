namespace Eron.Core.ManagementSettings
{
    public static class ApplicationFolder
    {
        public enum ApplicationFolderName
        {
            DefaultUploadFolder,
            ImageUploadFolder
        }

        public static string DefaultUploadFolder = "/Uploads/Default/";
        public static string ImageUploadFolder = "/Uploads/Images/";
    }
}