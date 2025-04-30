using efto_model.Models.DataTransferObjects;
using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace efto_window.Services
{
    public static class ImageService
    {
        public static async Task<DimensionRecord<uint>> GetDimensions(string imagePath)
        {
            try
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(imagePath));

                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);
                    return new(decoder.PixelWidth, decoder.PixelHeight);
                }
            }
            catch
            {
                return new(0, 0);
            }
        }
    }
}
