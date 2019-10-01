using Kros.KORM;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;

namespace Sample.CloundDesignPatterns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCatalogController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly CloudStorageAccount _cloudStorageAccount;

        public ImageCatalogController(IDatabase database)
        {
            _database = database;
            _cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=zmaztesting;AccountKey=YcZYJhMr2UdJ3BGzOByLGnQI9olGyzL/y2lCf92WhEWlWPVZRk+5kfTVAlod7AA524lnHCGHqhg+CfLDWe7Esg==;EndpointSuffix=core.windows.net");
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostAsync(PhotoViewModel viewModel)
        {
            Photo photo = viewModel.Adapt<Photo>();
            photo.Name = GetBlobName(photo.Name);

            await _database.AddAsync(photo);
            StorageDocumentSas storageDocument = GetSharedAccessReferenceForUpload(photo.Name);

            return Created(string.Empty, new { photo.Id, storageDocument });
        }

        private StorageDocumentSas GetSharedAccessReferenceForUpload(string blobName)
        {
            CloudBlockBlob blob = GetBlockBlobReference(blobName);
            SharedAccessBlobPolicy policy = SharedAccessBlobPolicyFactory.Create(SharedAccessBlobPermissions.Write);
            string blobCredentials = blob.GetSharedAccessSignature(policy);

            return new StorageDocumentSas(blobCredentials, blob.Uri, blobName);
        }

        private CloudBlockBlob GetBlockBlobReference(string blobName)
            => _cloudStorageAccount
            .CreateCloudBlobClient()
            .GetContainerReference("photos")
            .GetBlockBlobReference(blobName);

        private static class SharedAccessBlobPolicyFactory
        {
            public static SharedAccessBlobPolicy Create(SharedAccessBlobPermissions permissions)
                => new SharedAccessBlobPolicy
                {
                    Permissions = permissions,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5)
                };
        }

        private string GetBlobName(string documentName)
            => $"{Guid.NewGuid()}_{documentName}";
    }
}