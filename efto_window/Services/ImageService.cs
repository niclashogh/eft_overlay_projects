using efto_model.Data;
using efto_model.Models.Enums;
using efto_model.Records;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace efto_window.Services
{
    public static class ImageService
    {
        public static async Task<DimensionRecord<uint>> GetDimensions(ImageFolders folder, string fileName)
        {
            try
            {
                string filePath = Path.Combine(AssetContext.ApplicationFolder, folder.ToString(), fileName + ".png");
                StorageFile file = await StorageFile.GetFileFromPathAsync(filePath);

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

        public static void OpenAssestsFolder(ImageFolders folder)
        {
            string folderPath = Path.Combine(AssetContext.ApplicationFolder, folder.ToString());

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (Directory.Exists(folderPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = folderPath,
                    UseShellExecute = true
                });
            }
        }

        public static async Task PickImage(ImageFolders folder, nint windowHandle)
        {
            FileOpenPicker picker = new();
            picker.FileTypeFilter.Add(".png");
            picker.SuggestedStartLocation = PickerLocationId.Desktop;

            WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandle);

            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();

            if (files != null && files.Count > 0)
            {
                string folderPath = Path.Combine(AssetContext.ApplicationFolder, folder.ToString());
                Directory.CreateDirectory(folderPath);

                foreach (StorageFile file in files)
                {
                    string filePath = Path.Combine(folderPath, file.Name);
                    using (Stream src = await file.OpenStreamForReadAsync())
                    {
                        using (FileStream target = new(filePath, FileMode.Create, FileAccess.Write))
                        {
                            await src.CopyToAsync(target);
                        }
                    }
                }
            }
        }

        public static async Task<string> GetImageDateFeedback(ImageFolders folder, string fileName)
        {
            try
            {
                string filePath = Path.Combine(AssetContext.ApplicationFolder, folder.ToString(), fileName + ".png");
                StorageFile? file = await StorageFile.GetFileFromPathAsync(filePath);

                if (file != null)
                {
                    BasicProperties properties = await file.GetBasicPropertiesAsync();
                    return $"{properties.DateModified.DateTime.ToString()}";
                }
                else return "No image is available.";
            }
            catch { return "No image is available."; }
        }
    }
}
