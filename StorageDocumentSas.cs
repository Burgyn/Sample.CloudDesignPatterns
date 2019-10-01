using Kros.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.CloundDesignPatterns
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
