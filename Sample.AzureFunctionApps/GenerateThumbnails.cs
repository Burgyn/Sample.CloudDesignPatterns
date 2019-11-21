using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace Sample.AzureFunctionApps
{
    public static class GenerateThumbnails
    {
        [FunctionName("GenerateThumbnails")]
        public static void Run([BlobTrigger("photos/{name}")]Stream myBlob,
            [Blob("photos-thumbnails/{name}", FileAccess.Write)] Stream output, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n \n Size: {myBlob.Length} Bytes");

            IImageFormat format;

            using (Image input = Image.Load(myBlob, out format))
            {
                ResizeImage(input, output, format);
            }
        }

        public static void ResizeImage(Image input, Stream output, IImageFormat format)
        {
            var dimensions = (320, 200);

            input.Mutate(x => x.Resize(dimensions.Item1, dimensions.Item2));
            input.Save(output, format);
        }
    }
}
