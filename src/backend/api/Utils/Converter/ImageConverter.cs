using System;
using System.IO;

namespace api.Utils.Converter
{
    public static class ImageConverter
    {
        public static string? ConvertImageToAscii(string imagePath)
        {
            string asciiString = "";

            try
            {
                // Read the image file into a byte array
                byte[] imageData = File.ReadAllBytes(imagePath);

                // Convert each byte to its binary representation and group into 8-bit segments
                foreach (byte b in imageData)
                {
                    string binarySegment = Convert.ToString(b, 2).PadLeft(8, '0');
                    asciiString += binarySegment;
                }

                // Group the binary data into ASCII 8-bit characters
                string asciiChars = "";
                for (int j = 0; j < asciiString.Length; j += 8)
                {
                    string segment = asciiString.Substring(j, 8);
                    int asciiValue = Convert.ToInt32(segment, 2);
                    asciiChars += (char)asciiValue;
                }

                return asciiChars;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting image to ASCII: {ex.Message}");
                return null;
            }
        }
    }
}
