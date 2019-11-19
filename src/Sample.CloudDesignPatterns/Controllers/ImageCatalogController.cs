using Kros.KORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ThumbnailSharp;

namespace Sample.CloudDesignPatterns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCatalogController : ControllerBase
    {
        private readonly IDatabase _database;

        public ImageCatalogController(IDatabase database)
        {
            _database = database;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostAsync([FromForm]PhotoViewModel viewModel)
        {
            if (viewModel.Image.Length != 0)
            {
                var photo = new Photo() { Description = viewModel.Description };

                using (var ms = new MemoryStream())
                {
                    viewModel.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    photo.Image = fileBytes;
                    photo.Thumbnail = new ThumbnailCreator().CreateThumbnailBytes(20, fileBytes, Format.Jpeg);
                };

                await _database.AddAsync(photo);

                await SendEmail();

                return Created(string.Empty, new { photo.Id });
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static async Task SendEmail()
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("email", "meno"),
                Subject = "Success",
                PlainTextContent = "Váš obrázok bol spracovaný!",
                HtmlContent = "<strong>Váš obrázok bol spracovaný!</strong>"
            };
            msg.AddTo(new EmailAddress("email", "meno"));
            await client.SendEmailAsync(msg);
        }
    }
}
