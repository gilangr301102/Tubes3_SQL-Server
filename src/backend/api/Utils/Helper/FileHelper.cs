using api.Models;
using SixLabors.ImageSharp.PixelFormats;
using System.Text.Json;


namespace api.Utils.Helper
{
    public class FileHelper
    {
        public static string[] GetAllImagePaths()
        {
            var datasetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Migrations/Dataset");
            var imagePaths = Directory.GetFiles(datasetDirectory, "*.BMP");

            return imagePaths;
        }

        public static DataModel? LoadDataFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<DataModel>(json);
        }

        public static void ConvertBmpToPng(int idImage)
        {
            var imagePaths = GetAllImagePaths();

            // Ensure the file paths are valid
            if (!File.Exists(imagePaths[idImage]))
            {
                Console.WriteLine($"The BMP file at {imagePaths[idImage]} does not exist.");
                return;
            }

            try
            {
                // Load the BMP image
                using (Image<Rgba32> image = Image.Load<Rgba32>(imagePaths[idImage]))
                {
                    // Save the image as PNG
                    image.Save("reconstructed_image.png");
                    Console.WriteLine($"Successfully saved the image as PNG at {"reconstructed_image.png"}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while converting BMP to PNG: {ex.Message}");
            }
        }
    }
}
