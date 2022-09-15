using Microsoft.AspNetCore.Mvc;
using NotinoHomeWork.Application;
using NotinoHomeWork.Application.Providers.SerializerProvider;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace NotinoHomeWork.Api.Controllers
{
    /// <summary>
    /// Práce s dokumentem.
    /// </summary>
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class DocumentController : ControllerBase
    {
        private IHomeWorkModule homeWorkModule;
        public DocumentController(IHomeWorkModule homeWorkModule)
        {
            this.homeWorkModule = homeWorkModule;
        }


        /// <summary>
        /// Převod datového typu dokumentu.
        /// </summary>
        /// <param name="fromFormat">Z jakého formátu.</param>
        /// <param name="toFormat">Do jakého formátu.</param>
        /// <param name="file">Soubor k převodu.</param>
        /// <returns></returns>
        [HttpPost("Convert")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Convert([FromQuery, Required] DataTypeEnum fromFormat, [FromQuery, Required] DataTypeEnum toFormat, [Required] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            if (fromFormat == toFormat)
            {
                return BadRequest("Invalid conversion");
            }

            string stringData;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                stringData = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            var data = homeWorkModule.DeserializeDocument(fromFormat, stringData);

            var convertedFile = homeWorkModule.SerializeDocument(toFormat, data);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(convertedFile));

            return new FileStreamResult(stream, "application/octet-stream");
        }

        /// <summary>
        /// Načtení souboru podle cesty.
        /// </summary>
        /// <param name="filePath">Cesta k souboru.</param>
        /// <returns></returns>
        [HttpGet("File")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFileFromPath([FromQuery, Required] string filePath)
        {
            var data = await homeWorkModule.ReadFileFromPath(filePath);
            
            return new FileStreamResult(new MemoryStream(data), "application/octet-stream")
            {
                FileDownloadName = Path.GetFileName(filePath)
            };
        }

        /// <summary>
        /// Uložení souboru.
        /// </summary>
        /// <param name="filePath">Cesta kam soubor uložit.</param>
        /// <param name="file">Soubor.</param>
        /// <returns></returns>
        [HttpPost("File")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostFileToPath([FromQuery, Required] string filePath, [Required] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            byte[] data;
            using (var stream = new MemoryStream()) 
            { 
                await file.CopyToAsync(stream).ConfigureAwait(false);
                data = stream.ToArray();
            }

            await homeWorkModule.WriteFileToPath(filePath, data);

            return Ok();
        }

        /// <summary>
        /// Stažení dat z adresy.
        /// </summary>
        /// <param name="uri">Adresa.</param>
        /// <returns></returns>
        [HttpGet("Url")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DownloadData([FromQuery, Required] Uri uri)
        {
            var data = await homeWorkModule.GetDataFromUrl(uri);

            return new FileStreamResult(new MemoryStream(data), "application/octet-stream")
            {
                FileDownloadName = uri.ToString()
            };
        }

        /// <summary>
        /// Odeslání souboru na zadaný email.
        /// </summary>
        /// <param name="toEmail">Email na který se má soubor odeslat.</param>
        /// <param name="filePath">Soubor.</param>
        /// <returns></returns>
        [HttpPost("Email")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult EmailFile([FromQuery, Required] string toEmail, [FromQuery, Required] string filePath)
        {
            homeWorkModule.EmailFile(toEmail, filePath);
            return Ok();
        }
    }
}
