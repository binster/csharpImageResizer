using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace Web_Services.Services
{
    public class Image_Resizer : IImage_Resizer
    {

        public Image ResizeLargeImage(Image image, int width, int height)
        {
            //if image dimensions exceeds the max width or height passed in the arguments, resize it, else return image

            if ((image.Width > width) || (image.Height > height))
            {
                int originalHeight = image.Height;
                int originalWidth = image.Width;

                double ratioX = (double)width / originalWidth;
                double ratioY = (double)height / originalHeight;

                //Determine whether to use x-ratio or y-ratio as the limiting factor

                double ratio = ratioX < ratioY ? ratioX : ratioY;

                int resizedWidth = Convert.ToInt32(image.Width * ratio);
                int resizedHeight = Convert.ToInt32(image.Height * ratio);

                //Create new Bitmap with correct proportions

                Bitmap resizedImage = new Bitmap(resizedWidth, resizedHeight);

                //To retain high quality of the picture, use Graphics Class to draw new image

                resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    //Draw the image from the original image and dimensions to the new image with resized dimensions

                    graphics.DrawImage(image, new Rectangle(0, 0, resizedWidth, resizedHeight), new Rectangle(0, 0, originalWidth, originalHeight), GraphicsUnit.Pixel);
                }
                return resizedImage;
            }
            else
            {
                return image;
            }
        }
    }
}