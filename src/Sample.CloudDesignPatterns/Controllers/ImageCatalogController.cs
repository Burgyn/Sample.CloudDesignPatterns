using Kros.KORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThumbnailSharp;

namespace Sample.CloudDesignPatterns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCatalogController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly IOptions<EmailOptions> _mailOptions;

        public ImageCatalogController(IDatabase database, IOptions<EmailOptions> mailOptions)
        {
            _database = database;
            _mailOptions = mailOptions;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Photo>))]
        public IEnumerable<Photo> Get()
            => _database.Query<Photo>().Select("Id", "Name", "Description");

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostAsync([FromForm]PhotoViewModel viewModel)
        {
            if (viewModel.Image.Length != 0)
            {
                var photo = new Photo() { Description = viewModel.Description, Name = viewModel.Name };

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

        private async Task SendEmail()
        {
            var client = new SendGridClient(_mailOptions.Value.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_mailOptions.Value.Sender),
                Subject = "Success",
                PlainTextContent = "Váš obrázok bol spracovaný!",
                HtmlContent = "<strong>Váš obrázok bol spracovaný!</strong>"
            };
            msg.AddTo(new EmailAddress(_mailOptions.Value.Receiver));
            await client.SendEmailAsync(msg);
        }
    }
}
