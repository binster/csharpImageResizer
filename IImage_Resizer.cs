using System.Drawing;

namespace Web_Services.Services
{
    public interface IImage_Resizer
    {
        Image ResizeLargeImage(Image image, int width, int height);
    }
}