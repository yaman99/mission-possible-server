using MissionPossible.Application.Common.Interfaces.Services;

namespace MissionPossible.API.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileUploadService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> CreateFileAsync(IFormFile FileObject)
        {
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(FileObject.FileName) + "-" + Guid.NewGuid();
                var fileExtension = Path.GetExtension(FileObject.FileName).ToLower();

                //if (!CheckFileSize(FileObject.Length, MaxSize))
                //    throw new Exception();

                string path = _hostingEnvironment.WebRootPath + "\\uploads\\";


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fileStream = File.Create(path + fileName + fileExtension))
                {
                    await FileObject.CopyToAsync(fileStream);
                    fileStream.Flush();
                    return Path.Combine(path + fileName + fileExtension).Replace(_hostingEnvironment.WebRootPath, "").Replace("\\", "/");
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        private bool CheckFileSize(long fileSize, int maxSize)
        {
            if (fileSize > maxSize)
                return false;
            return true;
        }
        private string RenameFiles(string fileName, string fileExtension)
        {
            return fileName.ToString()
                .Replace(
                    fileName,
                    Guid.NewGuid().ToString() +
                    fileExtension
                );
        }
    }
}
