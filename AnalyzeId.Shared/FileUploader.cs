using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeId.Shared
{
    public class FileUploader : IFileUploader
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private ILogger logger = LogManager.GetCurrentClassLogger();

        public static string FilesPath { get; private set; }
        public FileUploader(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            FilesPath = $"{hostingEnvironment.WebRootPath}\\Files";
            if (!Directory.Exists(FilesPath))
            {
                Directory.CreateDirectory(FilesPath);
            }
        }

        public async Task<List<string>> UploadFiles(List<IFormFile> files)
        {
            try
            {
                List<string> names = new List<string>();
                //var filesPath = $"{hostingEnvironment.WebRootPath}\\Files";
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');
                    //if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
                    //{
                    //    fileName += ".png";
                    //}
                    fileName = Guid.NewGuid().ToString() + fileName;
                    var fullFilePath = Path.Combine(FilesPath, fileName);

                    if (file.Length <= 0)
                    {
                        continue;
                    }


                    int oneMeg = 1020 * 1024;
                    if (file != null && file.Length > oneMeg * 5)
                    {
                        using (var memory = new MemoryStream())
                        {
                            await file.CopyToAsync(memory);
                            using (var Image = new MagickImage(memory.ToArray()))
                            {
                                int maxWidth = 2000;
                                if (Image.Width > maxWidth)
                                {
                                    var average = Image.Width / maxWidth;
                                    Image.Resize(maxWidth, (Image.Height / average));
                                }

                                Image.Quality = 90;
                                //Image.Resize(1500, 1200);

                                Image.Write(fullFilePath);
                            }
                            ImageCompers(fullFilePath);

                        }
                        names.Add("\\Files\\" + fileName);
                    }
                    else
                    {
                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            names.Add("\\Files\\" + fileName);
                        }
                    }
                   
                  
                }
                return names;
            }
            catch (Exception ex)
            {


                return null;
            }
        }
        public void ImageCompers(string path)
        {
            //using (var Image=new MagickImage(path))
            //{

            //    Image.Quality = 30;
            //    Image.Resize(1500, 1200);

            //}

            var image = new FileInfo(path);
            var optimize = new ImageOptimizer();
            optimize.Compress(image);
            image.Refresh();

        }
        public async Task<string> CheckAndUploadAsync(IFormFile file)
        {

            return file != null ? (await UploadFiles(new List<IFormFile> { file })).FirstOrDefault() : null;

        }

        public async Task<string> UploadFilesFromBase64(string base64)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString() + ".png";
                var fullFilePath = Path.Combine(FilesPath, fileName);

                File.WriteAllBytes(fullFilePath, Convert.FromBase64String(base64));


                return "\\Files\\" + fileName;
            }
            catch (Exception ex)
            {
                logger.Debug("base64: " + base64);
                logger.Debug(ex);
                return null;
            }
        }

        public async Task<string> CheckAndUploadBase64Async(string base64)
        {
            try
            {
                if (base64.Contains(";"))
                {
                    base64 = base64.Split(";")[1].Replace("base64,", "");
                }
                return await UploadFilesFromBase64(base64);
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
                throw ex;
            }
        }


        public void DeleteFiles(List<string> files)
        {
            foreach (var file in files)
            {
                var path = $"{hostingEnvironment.WebRootPath}" + file;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
