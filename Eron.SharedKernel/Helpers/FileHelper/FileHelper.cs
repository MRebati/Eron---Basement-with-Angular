using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.ManagementSettings;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.Contract.UnitOfWorks;

namespace Eron.SharedKernel.Helpers.FileHelper
{
    public class FileHelper : IFileHelper
    {
        private IManagementUnitOfWork _unitOfWork;

        public FileHelper(IManagementUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EronFile SaveFile(HttpPostedFileBase file, ApplicationFolder.ApplicationFolderName folderName)
        {
            if (file != null && file.ContentLength > 0)
            {
                var inputFileName = file.FileName;
                var fileId = Guid.NewGuid();
                var fileName = fileId.ToString();

                var fullFolderName = GetFolderName(folderName);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(fullFolderName)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(fullFolderName));

                var server = HttpContext.Current.Server.MapPath(fullFolderName);

                var fileNameWithFolder = Path.Combine(server, fileName);

                var result = new EronFile()
                {
                    FileName = inputFileName,
                    FileUrl = fullFolderName + fileName,
                    Id = fileId,
                    FileType = GetFileType(file),
                    UploadDateTime = DateTime.Now
                };

                file.SaveAs(fileNameWithFolder);
                _unitOfWork.FileRepository.Create(result);
                _unitOfWork.Save();

                return result;
            }
            throw new FileNotFoundException("File is not found");
        }

        public async Task<EronFile> SaveFileAsync(HttpPostedFileBase file, ApplicationFolder.ApplicationFolderName folderName)
        {
            if (file != null && file.ContentLength > 0)
            {
                var inputFileName = file.FileName;
                var fileId = Guid.NewGuid();
                var fileName = fileId.ToString();

                var fullFolderName = GetFolderName(folderName);

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(fullFolderName)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(fullFolderName));

                var server = HttpContext.Current.Server.MapPath(fullFolderName);

                var fileNameWithFolder = Path.Combine(server, fileName);

                var result = new EronFile()
                {
                    FileName = inputFileName,
                    FileUrl = fullFolderName + fileName,
                    Id = fileId,
                    FileType = GetFileType(file),
                    UploadDateTime = DateTime.Now
                };

                file.SaveAs(fileNameWithFolder);
                _unitOfWork.FileRepository.Create(result);
                await _unitOfWork.SaveAsync();

                return result;
            }
            throw new FileNotFoundException("File is not found");
        }

        public bool TransferToDatabase(EronFile input)
        {
            var fileEntity = _unitOfWork.FileRepository.GetById(input.Id);
            if (fileEntity?.FileUrl == null)
                return false;
            //throw new FileNotFoundException();

            var shortFileAddress = input.FileUrl;
            var fileAddress = HttpContext.Current.Server.MapPath(shortFileAddress);
            if (fileAddress == null)
                return false;
            //throw new FileNotFoundException();

            var fileBytes = File.ReadAllBytes(fileAddress);
            fileEntity.FileData = fileBytes;
            fileEntity.FileUrl = null;
            File.Delete(fileAddress);
            _unitOfWork.FileRepository.Update(fileEntity);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> TransferToDatabaseAsync(EronFile input)
        {
            var fileEntity = await _unitOfWork.FileRepository.GetByIdAsync(input.Id);
            if (fileEntity?.FileUrl == null)
                return false;
            //throw new FileNotFoundException();

            var shortFileAddress = input.FileUrl;
            var fileAddress = HttpContext.Current.Server.MapPath(shortFileAddress);
            if (fileAddress == null)
                return false;
            //throw new FileNotFoundException();

            var fileBytes = File.ReadAllBytes(fileAddress);
            fileEntity.FileData = fileBytes;
            fileEntity.FileUrl = null;
            File.Delete(fileAddress);
            _unitOfWork.FileRepository.Update(fileEntity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public bool TransferToDatabase(EronFile input, IManagementUnitOfWork unitOfWork)
        {
            var fileEntity = _unitOfWork.FileRepository.GetById(input.Id);
            if (fileEntity?.FileUrl == null)
                return false;
            //throw new FileNotFoundException();

            var shortFileAddress = input.FileUrl;
            var fileAddress = HttpContext.Current.Server.MapPath(shortFileAddress);
            if (fileAddress == null)
                return false;
            //throw new FileNotFoundException();

            var fileBytes = File.ReadAllBytes(fileAddress);
            fileEntity.FileData = fileBytes;
            fileEntity.FileUrl = null;
            File.Delete(fileAddress);
            unitOfWork.FileRepository.Update(fileEntity);
            return true;
        }

        public async Task<bool> TransferToDatabaseAsync(EronFile input, IManagementUnitOfWork unitOfWork)
        {
            var fileEntity = await _unitOfWork.FileRepository.GetByIdAsync(input.Id);
            if (fileEntity?.FileUrl == null)
                return false;
            //throw new FileNotFoundException();

            var shortFileAddress = input.FileUrl;
            var fileAddress = HttpContext.Current.Server.MapPath(shortFileAddress);
            if (fileAddress == null)
                return false;
            //throw new FileNotFoundException();

            var fileBytes = File.ReadAllBytes(fileAddress);
            fileEntity.FileData = fileBytes;
            fileEntity.FileUrl = null;
            File.Delete(fileAddress);
            unitOfWork.FileRepository.Update(fileEntity);
            return true;
        }

        public EronFile GetFile(Guid fileId)
        {
            var fileEntity = _unitOfWork.FileRepository.GetById(fileId);
            return fileEntity;
        }

        public EronFile GetFile(string fileId)
        {
            var id = Guid.Parse(fileId);
            var fileEntity = _unitOfWork.FileRepository.GetById(id);
            return fileEntity;
        }

        public async Task<EronFile> GetFileAsync(Guid fileId)
        {
            var fileEntity = await _unitOfWork.FileRepository.GetByIdAsync(fileId);
            return fileEntity;
        }

        public async Task<EronFile> GetFileAsync(string fileId)
        {
            var id = Guid.Parse(fileId);
            var fileEntity = await _unitOfWork.FileRepository.GetByIdAsync(id);
            return fileEntity;
        }

        public List<EronFile> GetFilesInDirectory(ApplicationFolder.ApplicationFolderName folderName)
        {
            throw new NotImplementedException();
        }

        public Task<List<EronFile>> GetFilesInDirectoryAsync(ApplicationFolder.ApplicationFolderName folderName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDirctory(ApplicationFolder.ApplicationFolderName folderName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDirctoryAsync(ApplicationFolder.ApplicationFolderName folderName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(Guid fileId)
        {
            var fileEntity = _unitOfWork.FileRepository.GetById(fileId);
            var fileAddress = HttpContext.Current.Server.MapPath(fileEntity.FileUrl);
            if (File.Exists(fileAddress))
            {
                File.Delete(fileAddress);
                fileEntity.Deleted = true;
                _unitOfWork.FileRepository.Update(fileEntity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteFileAsync(Guid fileId)
        {
            var fileEntity = await _unitOfWork.FileRepository.GetByIdAsync(fileId);

            if (System.IO.File.Exists(fileEntity.FileUrl))
            {
                File.Delete(fileEntity.FileUrl);
                fileEntity.Deleted = true;
                _unitOfWork.FileRepository.Update(fileEntity);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        #region Helpers

        private FileType GetFileType(HttpPostedFileBase file)
        {
            var fileType = Path.GetExtension(file.FileName);
            switch (fileType)
            {
                case ".jpg":
                    return FileType.Image;
                case ".mp3":
                    return FileType.Sound;
                case ".zip":
                    return FileType.Zip;
                case ".rar":
                    return FileType.Zip;
                case ".iso":
                    return FileType.Zip;
                case ".mp4":
                    return FileType.Video;
                default:
                    return FileType.Unknown;
            }
        }

        private string GetFolderName(ApplicationFolder.ApplicationFolderName folderName)
        {
            switch (folderName)
            {
                case ApplicationFolder.ApplicationFolderName.DefaultUploadFolder:
                    return ApplicationFolder.DefaultUploadFolder;
                case ApplicationFolder.ApplicationFolderName.ImageUploadFolder:
                    return ApplicationFolder.ImageUploadFolder;
                default:
                    throw new EntryPointNotFoundException("requested folder doesn't exist in the list");
            }
        }

        #endregion
    }
}
