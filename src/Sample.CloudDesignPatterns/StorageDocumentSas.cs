using Kros.Utils;
using System;

namespace Sample.CloudDesignPatterns
{
    public class StorageDocumentSas
    {
        public StorageDocumentSas(string credentials, Uri blobUri, string name)
        {
            Credentials = Check.NotNullOrWhiteSpace(credentials, nameof(credentials));
            BlobUri = Check.NotNull(blobUri, nameof(blobUri));
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        }

        public string Credentials { get; }

        public Uri BlobUri { get; }

        public string Name { get; }
    }
}
