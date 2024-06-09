using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace api.Utils.Converter
{
    public static class ImageConverter
    {
        private const double RedCoefficient = 0.299;
        private const double GreenCoefficient = 0.587;
        private const double BlueCoefficient = 0.114;

        public static int[,] BitmapToBinaryMatrix(string imagePath)
        {
            Console.WriteLine($"Converting image at {imagePath} to binary matrix...");
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = image.Width;
                int height = image.Height;
                int[,] binaryValues = new int[width, height];

                using (var sha256 = SHA256.Create())
                {
                    // Convert the image to a byte array
                    byte[] imageData;
                    using (var stream = new MemoryStream())
                    {
                        image.SaveAsBmp(stream);
                        imageData = stream.ToArray();
                    }

                    // Compute the SHA-256 hash of the image byte array
                    byte[] hash = sha256.ComputeHash(imageData);

                    // Convert the hash to a binary matrix
                    int index = 0;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // Use only the least significant bit of each byte in the hash
                            int grayscaleValue = hash[index] & 0x01;
                            binaryValues[x, y] = grayscaleValue;
                            index = (index + 1) % hash.Length;
                        }
                    }
                }

                Console.WriteLine($"Conversion to binary matrix completed.");
                return binaryValues;
            }
        }

        public static string[] Get30PixelAscii(int[,] binaryValues)
        {
            Console.WriteLine("Converting binary matrix to 30-pixel ASCII segments...");
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

            Console.WriteLine("Conversion to 30-pixel ASCII segments completed.");
            return asciiSegments.ToArray();
        }

        public static string EncodeAsciiToBase64(string asciiString)
        {
            Console.WriteLine("Encoding ASCII to Base64...");
            byte[] asciiBytes = Encoding.ASCII.GetBytes(asciiString);
            string base64String = Convert.ToBase64String(asciiBytes);
            Console.WriteLine("Encoding to Base64 completed.");
            return base64String;
        }

        public static void Main()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            string relativePath = @"Dataset/1__M_Left_index_finger_CR.BMP";
            string absolutePath = Path.GetFullPath(relativePath);

            Console.WriteLine($"Absolute Path: {absolutePath}");

            int[,] temp = BitmapToBinaryMatrix(absolutePath);
            string[] asciiSegments = Get30PixelAscii(temp);

            for (int i = 0; i < asciiSegments.Length; i++)
            {
                Console.WriteLine($"Segment {i + 1} data (ASCII):");
                Console.WriteLine(asciiSegments[i]);
            }
        }
    }
}

