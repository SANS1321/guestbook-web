using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace GuestBook.Web.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _container;

        public BlobStorageService(IConfiguration config)
        {
            var connStr = config["AzureStorage:ConnectionString"];
            var blobService = new BlobServiceClient(connStr);
            _container = blobService
                .GetBlobContainerClient("guestbookpics");
            _container.CreateIfNotExists(PublicAccessType.Blob);
        }

        public async Task<string> UploadPhotoAsync(
            IFormFile file)
        {
            string blobName = $"photo_{Guid.NewGuid()}.jpg";
            var blob = _container.GetBlobClient(blobName);
            using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);
            return blob.Uri.ToString();
        }
    }
}
