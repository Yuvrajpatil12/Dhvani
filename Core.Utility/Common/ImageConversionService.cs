using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;


namespace Core.Utility.Common
{
    public  class ImageConversionService
    {


       

        //    private readonly string _uploadPath;

        //public ImageConversionService(IHostingEnvironment env)
        //{
        //    _uploadPath = env.WebRootPath;

        //}

        /// <summary>
        /// This only for use the image conversion service webp and jpg format
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        //public async Task ConvertAndSaveImageAsync(IFormFile file, string fileName)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        throw new ArgumentException("Invalid file provided.");
        //    }

        //    try
        //    {
        //        string originalExtension = Path.GetExtension(file.FileName);
        //        if (!IsValidImageExtension(originalExtension))
        //        {
        //            throw new ArgumentException("Unsupported image format. Only JPEG, PNG, or BMP are allowed.");
        //        }

        //        string uniqueFileName = GenerateUniqueFileName(originalExtension);
        //        string originalFilePath = Path.Combine(_uploadPath, uniqueFileName);

        //        // Save the original image
        //        await using (var originalFileStream = new FileStream(originalFilePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(originalFileStream);
        //        }

        //        // Convert to WebP and JPG simultaneously
        //        await Task.WhenAll(
        //            ConvertToWebPAsync(originalFilePath, GetWebPFilePath(uniqueFileName)),
        //            ConvertToJpgAsync(originalFilePath, GetJpgFilePath(uniqueFileName))
        //        );


        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        //private bool IsValidImageExtension(string extension)
        //{
        //    return new[] { ".jpg", ".jpeg", ".png", ".bmp" }.Contains(extension.ToLower());
        //}

        //private string GenerateUniqueFileName(string originalExtension)
        //{
        //    return Guid.NewGuid().ToString() + originalExtension;
        //}

        //private string GetWebPFilePath(string uniqueFileName)
        //{
        //    return Path.Combine(_uploadPath, Path.ChangeExtension(uniqueFileName, ".webp"));
        //}

        //private string GetJpgFilePath(string uniqueFileName)
        //{
        //    return Path.Combine(_uploadPath, Path.ChangeExtension(uniqueFileName, ".jpg"));
        //}

        //private async Task ConvertToWebPAsync(string sourcePath, string targetPath)
        //{
        //    using (var image = await Image.LoadAsync(sourcePath))
        //    {
        //        using (var webPFileStream = new FileStream(targetPath, FileMode.Create))
        //        {
        //            image.Save(webPFileStream, new WebpEncoder());
        //        }
        //    }
        //}

        //private async Task ConvertToJpgAsync(string sourcePath, string targetPath)
        //{
        //    using (var image = await Image.LoadAsync(sourcePath))
        //    {
        //        using (var jpgFileStream = new FileStream(targetPath, FileMode.Create))
        //        {
        //            image.Save(jpgFileStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder() { Quality = 90 });
        //            // Provide Quality in the constructor
        //        }
        //    }
        //}

    }
}
