using System.IO;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace WebUIAD.Services
{
    public class ImageService
    {

        private readonly BlobServiceClient blobServiceClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// Constructor for Image Service.
        /// </summary>
        /// <param name="configuration">Iconfiguration.</param>
        public ImageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureStorageConnection");

            var blobClientOptions = new BlobClientOptions
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential,
                },
            };
            this.blobServiceClient = new BlobServiceClient(connectionString, blobClientOptions);
        }


        /// <inheritdoc/>
        public async Task DeleteImageAsync(string containerName, string fileName)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }


        /// <inheritdoc/>
        public async Task<Stream> DownloadImageAsync(string containerName, string fileName)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var response = await blobClient.OpenReadAsync();
            return response;
        }

        /// <inheritdoc/>
        public async Task<Stream> ResizeImageAsync(Stream originalStream, int maxWidth, int maxHeight, string fileName)
        {

            using (var originalImage = await Image.LoadAsync(originalStream))
            {
                originalImage.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(maxWidth, maxHeight),
                    Mode = ResizeMode.Max,
                }));

                var resizedImageStream = new MemoryStream();

                var fileExtension = Path.GetExtension(fileName)?.ToLowerInvariant();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                {
                    await originalImage.SaveAsync(resizedImageStream, new JpegEncoder());
                }
                else if (fileExtension == ".png")
                {
                    await originalImage.SaveAsync(resizedImageStream, new PngEncoder());
                }
                else
                {
                    // Handle other image formats as needed
                    throw new NotSupportedException($"Unsupported image format: {fileExtension}");
                }
                resizedImageStream.Position = 0;
                return resizedImageStream;
            }
        }

        /// <inheritdoc/>
        public async Task<string> UploadAndResizeImageAsync(Stream originalImageStream, string containerName, string fileName, int maxWidth, int maxHeight)
        {
            // Resize image
            var resizedImageStream = await this.ResizeImageAsync(originalImageStream, maxWidth, maxHeight, fileName);

            // Upload resized image to Azure Storage
            var uri = await this.UploadImageAsync(resizedImageStream, containerName, fileName);
            return uri;
        }

        /// <inheritdoc/>
        public async Task<string> UploadImageAsync(Stream imageStream, string containerName, string fileName)
        {
            var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(imageStream, true);
            return blobClient.Uri.ToString();
        }
    }
}
