using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Sample.AzureFunctionApps
{
    public static class EmailNotification
    {
        [FunctionName("EmailNotification")]
        public static void Run([ServiceBusTrigger("imagecatalog", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
