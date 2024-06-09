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
        public static byte[,] BitmapToBinaryMatrix(string imagePath)
        {
            Console.WriteLine($"Converting image at {imagePath} to binary matrix...");
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = image.Width;
                int height = image.Height;
                byte[,] binaryValues = new byte[width, height];

                // Convert the image to grayscale
                image.Mutate(ctx => ctx.Grayscale());

                // Perform adaptive thresholding
                double[,] grayscaleValues = new double[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = image[x, y];
                        grayscaleValues[x, y] = pixel.R / 255.0; // Normalize to [0, 1]
                    }
                }

                // Calculate local mean and threshold
                int blockSize = 15; // Block size for local thresholding
                double c = 0.01;    // Constant subtracted from the mean

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        double localMean = GetLocalMean(grayscaleValues, x, y, blockSize, width, height);
                        double threshold = localMean - c;
                        binaryValues[x, y] = grayscaleValues[x, y] > threshold ? (byte)1 : (byte)0;
                    }
                }

                Console.WriteLine($"Conversion to binary matrix completed.");
                Console.WriteLine($"Length of each binary row: {width}");
                Console.WriteLine($"Length of each binary column: {height}");
                return binaryValues;
            }
        }

        public static double GetLocalMean(double[,] grayscaleValues, int x, int y, int blockSize, int width, int height)
        {
            int halfSize = blockSize / 2;
            int minX = Math.Max(0, x - halfSize);
            int maxX = Math.Min(width - 1, x + halfSize);
            int minY = Math.Max(0, y - halfSize);
            int maxY = Math.Min(height - 1, y + halfSize);

            double sum = 0.0;
            int count = 0;
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    sum += grayscaleValues[i, j];
                    count++;
                }
            }
            return sum / count;
        }
        public static byte[] Get8ByteBinary(byte[,] binaryValues)
        {
            Console.WriteLine("Converting binary matrix to 8 Bit Binary segments...");
            int width = binaryValues.GetLength(0);
            int height = binaryValues.GetLength(1);
            List<byte> asciiSegments = new List<byte>();

            for (int startY = 0; startY < height; startY++)
            {
                string tempBits = "";
                for (int startX = 0; startX < width; startX++)
                {
                    tempBits += binaryValues[startX, startY];
                }
                if (tempBits != "001111111111111111111111111111111111111111111111111111111111111111111111111111111111111111110000" &&
                    tempBits != "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" &&
                    tempBits != "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001110000")
                {
                    //Console.WriteLine(tempBits);
                    // Create an initial mask for the first 8 bits in the row
                    ulong currentValue = 0;
                    for (int bit = 0; bit < 8; bit++)
                    {
                        currentValue |= ((ulong)binaryValues[bit, startY] << (7 - bit));
                    }

                    // Process the rest of the row
                    for (int startX = 0; startX <= width - 8; startX+=8)
                    {
                        if (startX > 0)
                        {
                            // Shift left by one and add the next bit on the right
                            currentValue = ((currentValue << 1) & 0xFF) | binaryValues[startX + 7, startY];
                        }

                        byte currentValueByte = (byte)(currentValue & 0xFF);
                        // Now you have your currentValue represented as a byte
                        asciiSegments.Add(currentValueByte);
                    }
                }
            }

            Console.WriteLine("Conversion to 8-bit ASCII segments completed.");
            return asciiSegments.ToArray();
        }

        public static string GetASCII8Bit(string imagePath)
        {
            byte[,] binaryMatrix = BitmapToBinaryMatrix(imagePath);
            Console.WriteLine("Binary matrix:");
            for (int y = 0; y < 103; y++)
            {
                for (int x = 0; x < 96; x++)
                {
                    Console.Write(binaryMatrix[x, y]);
                }
                Console.WriteLine();
            }

            byte[] asciiSegments = Get8ByteBinary(binaryMatrix);

            string temp = "";

            for (int i = 0; i < asciiSegments.Length; i++)
            {
                byte byteValue = asciiSegments[i];
                temp += Convert.ToBase64String(new[] { byteValue });
                // temp.Append((char)byteValue);
                // temp += (char)byteValue;
            }
            return temp;
        }

        public static string[] Get30PixelAscii(string berkasCitra)
        {
            Console.WriteLine("Get Partition of binary matrix to 30-pixel ASCII segments...");

            int length = berkasCitra.Length / 24;

            List<string> stringRet = new List<string>();

            // get each 30-pixel segment
            for (int i = 0; i < length; i++)
            {
                string temp30Pixel = "";
                for(int k = 0; k < 24; k++)
                {
                    temp30Pixel += berkasCitra[i * 24 + k];
                }
                stringRet.Add(temp30Pixel);
            }

            Console.WriteLine("Get to 30-pixel partition of ASCII segments completed.");
            return stringRet.ToArray();
        }

        public static void SaveImageFromBinaryMatrix(byte[,] binaryMatrix, string outputPath)
        {
            int width = binaryMatrix.GetLength(0);
            int height = binaryMatrix.GetLength(1);
            using (var image = new Image<Rgba32>(width, height))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte value = binaryMatrix[x, y];
                        image[x, y] = value == 1 ? new Rgba32(0, 0, 0) : new Rgba32(255, 255, 255); // Black or White
                    }
                }
                image.Save(outputPath);
            }
        }

        public static byte[,] ConvertBase64ToBinaryMatrix(string base64String, int width, int height)
        {
            try
            {
                // Ensure that the base64 string only contains valid characters
                byte[] bytes = Convert.FromBase64String(base64String);
                byte[,] binaryMatrix = new byte[width, height];

                int byteIndex = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (byteIndex < bytes.Length)
                        {
                            binaryMatrix[x, y] = (byte)(bytes[byteIndex] == 0 ? 0 : 1);
                            byteIndex++;
                        }
                    }
                }

                return binaryMatrix;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid Base64 string: {base64String}");
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public static void Main()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            string relativePath = @"Dataset/7__M_Left_index_finger_CR.BMP";
            string absolutePath = Path.GetFullPath(relativePath);

            Console.WriteLine($"Absolute Path: {absolutePath}");

            string debugASCIIString = GetASCII8Bit(absolutePath);
            Console.WriteLine(debugASCIIString);
            Console.WriteLine(debugASCIIString.Length);
        }
    }
}

