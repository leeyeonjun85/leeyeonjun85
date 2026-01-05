using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RichTextBoxBasic.Utils
{
  public static class ImageUtils
  {
    public static BitmapImage ToBitmapImage(this BitmapSource bitmapSource)
    {
      BitmapImage bitmapImage = new BitmapImage();
      PngBitmapEncoder encoder = new PngBitmapEncoder();
      using (MemoryStream memoryStream = new())
      {
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        encoder.Save(memoryStream);
        memoryStream.Position = 0;
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memoryStream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
      }
      return bitmapImage;
    }
  }
}
