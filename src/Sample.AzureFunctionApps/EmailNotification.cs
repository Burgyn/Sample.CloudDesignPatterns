using Microsoft.Azure.WebJobs;
using SendGrid.Helpers.Mail;
using System;

namespace Sample.AzureFunctionApps
{
    public static class EmailNotification
    {
        [FunctionName("EmailNotification")]
        public static void Run(
            [ServiceBusTrigger("imagecatalog", Connection = "ServiceBusConnection")]string myQueueItem,
            [SendGrid(ApiKey = "SendGridKeyAppSettingName")] out SendGridMessage message)
        {                        
            message = new SendGridMessage();
            message.AddTo(Environment.GetEnvironmentVariable("MailTo"));
            message.AddContent("text/html", "Obrázok bol prijatý na spracovanie.");
            message.SetFrom(new EmailAddress(Environment.GetEnvironmentVariable("MailFrom")));
            message.SetSubject("Fri photo notification");
        }
    }
}
