using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web.Controllers.Api
{
    [RoutePrefix("api/resizer")]
    public class UploadController : ApiController
    {
        //inject Image Resizing Service
        readonly IImage_Resizer imageService;

        public UploadController(IImage_Resizer imageService)
        {
            this.imageService = imageService;
        }
        //async Post due to waiting for image to be read
        [Route, HttpPost]
        public async Task<HttpResponseMessage> UploadFile()
        {
            //check if file is a mimetype
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var fileToRead = await Request.Content.ReadAsMultipartAsync();

            byte[] file = await Stream.ReadAsByteArrayAsync(fileToRead);

            Image image = (Bitmap)((new ImageConverter()).ConvertFrom(file));
            Image newImage = resizeService.ResizeLargeImage(image, 1980, 1080);
            using (var ms = new MemoryStream())
            {
                newImage.Save(ms, ImageFormat.Png);
                newFile = ms.ToArray();
            }

            //instead of returning response, possibly store image to disk

            return Request.CreateResponse(HttpStatusCode.OK, "image resized";

        }

    }
}