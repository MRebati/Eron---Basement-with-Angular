using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.ManagementSettings;
using Eron.DataAccess.Contract.UnitOfWorks;

namespace Eron.SharedKernel.Helpers.FileHelper
{
    public interface IFileHelper
    {
        EronFile SaveFile(HttpPostedFileBase file, ApplicationFolder.ApplicationFolderName folderName);

        Task<EronFile> SaveFileAsync(HttpPostedFileBase file, ApplicationFolder.ApplicationFolderName folderName);

        bool TransferToDatabase(EronFile file, IManagementUnitOfWork unitOfWork);

        Task<bool> TransferToDatabaseAsync(EronFile input, IManagementUnitOfWork unitOfWork);

        bool TransferToDatabase(EronFile file);

        Task<bool> TransferToDatabaseAsync(EronFile file);

        EronFile GetFile(Guid fileId);

        EronFile GetFile(string fileId);

        Task<EronFile> GetFileAsync(Guid fileId);

        Task<EronFile> GetFileAsync(string fileId);

        List<EronFile> GetFilesInDirectory(ApplicationFolder.ApplicationFolderName folderName);

        Task<List<EronFile>> GetFilesInDirectoryAsync(ApplicationFolder.ApplicationFolderName folderName);

        bool DeleteDirctory(ApplicationFolder.ApplicationFolderName folderName);

        Task<bool> DeleteDirctoryAsync(ApplicationFolder.ApplicationFolderName folderName);

        bool DeleteFile(Guid fileId);

        Task<bool> DeleteFileAsync(Guid fileId);
        
    }
}