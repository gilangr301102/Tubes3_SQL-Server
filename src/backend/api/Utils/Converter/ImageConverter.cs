using System;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace api.Utils.Converter
{
    public static class ImageConverter
    {
        // Define grayscale conversion coefficients
        private const double RedCoefficient = 0.299;
        private const double GreenCoefficient = 0.587;
        private const double BlueCoefficient = 0.114;

        // bitmap -> pengolahan bitmap keseluruhan
        public static int[,] BitmapToBinaryMatrix(string imagePath)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
            {
                int width = image.Width;
                int height = image.Height;
                int[,] binaryValues = new int[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Rgba32 pixelColor = image[x, y];
                        int grayscaleValue = (int)(pixelColor.R * RedCoefficient + pixelColor.G * GreenCoefficient + pixelColor.B * BlueCoefficient);
                        binaryValues[x, y] = (grayscaleValue < 128) ? 0 : 1;
                    }
                }

                return binaryValues;
            }
        }

        // array -> pengolahan 30 pixel
        public static string[] Get30PixelAscii(int[,] binaryValues)
        {
            int width = binaryValues.GetLength(0);
            int height = binaryValues.GetLength(1);
            List<string> asciiSegments = new List<string>();

            for (int startY = 0; startY <= height - 30; startY++)
            {
                for (int startX = 0; startX <= width - 30; startX++)
                {
                    StringBuilder asciiBuilder = new StringBuilder();

                    for (int y = startY; y < startY + 30; y++)
                    {
                        for (int x = startX; x < startX + 30; x++)
                        {
                            int grayscaleValue = binaryValues[x, y] * 255; // Convert binary to grayscale
                            asciiBuilder.Append((char)grayscaleValue); // Convert grayscale to ASCII character
                        }
                    }

                    asciiSegments.Add(asciiBuilder.ToString());
                }
            }

            return asciiSegments.ToArray();
        }

        // Encode ASCII 8-bit string to Base64
        public static string EncodeAsciiToBase64(string asciiString)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(asciiString);
            return Convert.ToBase64String(asciiBytes);
        }

        // contoh penggunaan
        public static void Main()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            // Define the relative path to the image from the utils folder
            string relativePath = @"Dataset/1__M_Left_index_finger_CR.BMP";
            string absolutePath = Path.GetFullPath(relativePath);

            // Print the absolute path to verify it is correct
            Console.WriteLine($"Absolute Path: {absolutePath}");

            int[,] temp = BitmapToBinaryMatrix(absolutePath);
            string[] asciiSegments = Get30PixelAscii(temp);

            // Output each segment data
            for (int i = 0; i < asciiSegments.Length; i++)
            {
                Console.WriteLine($"Segment {i + 1} data (ASCII):");
                Console.WriteLine(asciiSegments[i]);
            }
        }
    }
}
