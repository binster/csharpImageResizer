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
            if ((image.Width > width) || (image.Height > height))
            {
                int originalHeight = image.Height;
                int originalWidth = image.Width;

                double ratioX = (double)width / originalWidth;
                double ratioY = (double)height / originalHeight;

                //Determine whether to use x-ratio or y-ratio as the limiting factor

                double ratio = ratioX < ratioY ? ratioX : ratioY;

                int newHeight = Convert.ToInt32(image.Height * ratio);
                int newWidth = Convert.ToInt32(image.Width * ratio);
                Bitmap newImage = new Bitmap(newWidth, newHeight);

                newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, originalWidth, originalHeight), GraphicsUnit.Pixel);
                }
                return newImage;
            }
            else
            {
                return image;
            }
        }
    }
}