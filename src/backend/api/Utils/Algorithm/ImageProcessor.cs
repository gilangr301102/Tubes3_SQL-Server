using System.Drawing;
using System.Text;

namespace api.utils
{
    // public class ImageProcessor
    // {
    //     // bitmap -> pengolahan bitmap keseluruhan
    //     public static Bitmap BitmapToGrayscaleBitmap(Bitmap original)
    //     {
    //         Bitmap grayscale = new Bitmap(original.Width, original.Height);

    //         for (int y = 0; y < original.Height; y++)
    //         {
    //             for (int x = 0; x < original.Width; x++)
    //             {
    //                 Color originalColor = original.GetPixel(x, y);
    //                 int grayscaleValue = (int)(originalColor.R * 0.299 + originalColor.G * 0.587 + originalColor.B * 0.114);
    //                 Color newColor = Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue);
    //                 grayscale.SetPixel(x, y, newColor);
    //             }
    //         }

    //         return grayscale;
    //     }

    //     public static Bitmap GrayscaleBitmaptToBinaryBitmap(Bitmap grayscale)
    //     {
    //         Bitmap binary = new Bitmap(grayscale.Width, grayscale.Height);

    //         for (int y = 0; y < grayscale.Height; y++)
    //         {
    //             for (int x = 0; x < grayscale.Width; x++)
    //             {
    //                 Color pixelColor = grayscale.GetPixel(x, y);
    //                 int binaryValue = (pixelColor.R < 128) ? 0 : 255;
    //                 Color newColor = Color.FromArgb(binaryValue, binaryValue, binaryValue);
    //                 binary.SetPixel(x, y, newColor);
    //             }
    //         }

    //         return binary;
    //     }
    //     public static int[,] BitmapToBinaryMatrix(Bitmap bitmap)
    //     {
    //         int width = bitmap.Width;
    //         int height = bitmap.Height;
    //         int[,] binaryValues = new int[width, height];

    //         for (int y = 0; y < height; y++)
    //         {
    //             for (int x = 0; x < width; x++)
    //             {
    //                 Color pixelColor = bitmap.GetPixel(x, y);
    //                 int grayscaleValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
    //                 binaryValues[x, y] = (grayscaleValue < 128) ? 0 : 1;
    //             }
    //         }

    //         return binaryValues;
    //     }

    //     public static string BinaryMatrixToAscii(int[,] binaryValues)
    //     {
    //         int width = binaryValues.GetLength(0);
    //         int height = binaryValues.GetLength(1);
    //         StringBuilder asciiBuilder = new StringBuilder();

    //         for (int y = 0; y < height; y++)
    //         {
    //             for (int x = 0; x < width; x += 8) // Process 8 bits at a time
    //             {
    //                 if (x + 7 >= width) // Check if there are less than 8 bits remaining
    //                 {
    //                     break; // Break the loop if less than 8 bits remain
    //                 }

    //                 int asciiValue = 0;
    //                 for (int i = 0; i < 8; i++)
    //                 {
    //                     asciiValue += binaryValues[x + i, y] << (7 - i); // Convert binary to decimal
    //                 }

    //                 asciiBuilder.Append((char)asciiValue); // Convert decimal to ASCII character
    //             }
    //         }

    //         return asciiBuilder.ToString();
    //     }

    //     // array -> pengolahan 30 pixel
    //     public static Color[] Get30PixelColorArray(Bitmap image)
    //     {
    //         // cari 30 pixel berurutan yang paling variatif nilainya
    //         int maxUnique = 0;
    //         Color[] finalColorArray = { };

    //         for (int y = 0; y < image.Height; y++)
    //         {
    //             for (int x = 0; x < image.Width - 30; x++)
    //             {
    //                 int tempUnique = 0;
    //                 Color[] tempColorArray = Array.Empty<Color>();
    //                 for (int k = x; k < x + 30; k++)
    //                 {
    //                     Color pixelColor = image.GetPixel(k, y);
    //                     if (!IsInArray(pixelColor, tempColorArray))
    //                     {
    //                         tempColorArray.Append(pixelColor);
    //                         tempUnique++;
    //                     }
    //                 }
    //                 if (tempUnique > maxUnique)
    //                 {
    //                     maxUnique = tempUnique;
    //                     finalColorArray = tempColorArray;
    //                 }
    //             }
    //         }
    //         return finalColorArray;
    //     }

    //     public static int[] ColorArrayToBinaryArray(Color[] colors)
    //     {
    //         int[] binaryValues = new int[30];
    //         for (int i = 0; i < colors.Length; i++)
    //         {
    //             Color pixelColor = colors[i];
    //             int grayscaleValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
    //             binaryValues[i] = (grayscaleValue < 128) ? 0 : 1;
    //         }
    //         return binaryValues;
    //     }

    //     public static string BinaryArrayToAscii(int[] binaryValues)
    //     {
    //         if (binaryValues == null || binaryValues.Length == 0)
    //         {
    //             throw new ArgumentException("binaryValues array cannot be null or empty.");
    //         }

    //         StringBuilder asciiBuilder = new StringBuilder();

    //         for (int i = 0; i < binaryValues.Length; i += 8)
    //         {
    //             if (i + 8 > binaryValues.Length)
    //             {
    //                 break; // Not enough bits left to form a full byte
    //             }

    //             int asciiValue = 0;
    //             for (int j = 0; j < 8; j++)
    //             {
    //                 asciiValue += binaryValues[i + j] << (7 - j); // Convert binary to decimal
    //             }

    //             asciiBuilder.Append((char)asciiValue); // Convert decimal to ASCII character
    //         }

    //         return asciiBuilder.ToString();
    //     }

    //     // fungsi tambahan
    //     public static bool IsInArray(Color color, Color[] colorArray)
    //     {
    //         for (int i = 0; i < colorArray.Length; i++)
    //         {
    //             if (colorArray[i] == color)
    //             {
    //                 return true;
    //             }
    //         }
    //         return false;
    //     }

    //     // contoh penggunaan
    //     public static void main()
    //     {
    //         string currentDirectory = Directory.GetCurrentDirectory();
    //         Console.WriteLine($"Current Directory: {currentDirectory}");

    //         // Define the relative path to the image from the utils folder
    //         string relativePath = @"/api/Dataset/1__M_Left_index_finger_CR.BMP";
    //         string absolutePath = Path.GetFullPath(relativePath);

    //         // Print the absolute path to verify it is correct
    //         Console.WriteLine($"Absolute Path: {absolutePath}");

    //         Bitmap bitmap = new Bitmap(absolutePath);
    //         BitmapToGrayscaleBitmap(bitmap);
    //         bitmap = GrayscaleBitmaptToBinaryBitmap(bitmap);
    //         bitmap.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);
    //         int[,] temp = BitmapToBinaryMatrix(bitmap);
    //         String tempString = BinaryMatrixToAscii(temp);
    //         Console.WriteLine("Berhasil ASCII Binary Matrix!");
    //         Console.WriteLine(tempString);
    //         Console.WriteLine(tempString.Length);
    //         Console.WriteLine(temp.Length);
    //         Color[] temp30Pixels = Get30PixelColorArray(bitmap);
    //         Console.WriteLine("Berhasil 30 Pixel Color Array!");
    //         int[] temp30PixelsArray = ColorArrayToBinaryArray(temp30Pixels);
    //         Console.WriteLine("Berhasil 30 Pixel Binary Array!");
    //         for (int i = 0; i < temp30PixelsArray.Length; i++)
    //         {
    //             Console.Write(temp30PixelsArray[i]);
    //         }
    //         Console.WriteLine("");
    //         String temp30PixelString = BinaryArrayToAscii(temp30PixelsArray);
    //         Console.WriteLine("Berhasil 30 Pixel String!");
    //         Console.WriteLine(temp30PixelString);
    //     }
    // }
}