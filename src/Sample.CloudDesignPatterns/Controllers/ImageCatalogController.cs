using Kros.KORM;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using MassTransit;
using Sample.CloudDesignPatterns.Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Sample.CloudDesignPatterns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCatalogController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly IBusControl _bus;
        private readonly CloudStorageAccount _cloudStorageAccount;

        public ImageCatalogController(IDatabase database, IBusControl bus, IOptions<BlobStorageOptions> storageOptions)
        {
            _database = database;
            _bus = bus;
            _cloudStorageAccount = CloudStorageAccount.Parse(storageOptions.Value.ConnectionString);
        }

        [HttpGet]
        public IEnumerable<Photo> Get()
            => _database.Query<Photo>().Select(p => new { p.Id, p.Name, p.Description });

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostAsync(PhotoViewModel viewModel)
        {
            Photo photo = viewModel.Adapt<Photo>();
            photo.Name = GetBlobName(photo.Name);

            await _database.AddAsync(photo);
            StorageDocumentSas storageDocument = GetSharedAccessReferenceForUpload(photo.Name);

            await _bus.Publish<IImageAcceptedMessage>(new { photo.Name });

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
