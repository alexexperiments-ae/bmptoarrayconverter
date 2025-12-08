using System;
using System.Drawing;
using System.IO;

class BMPToArrayConverter
{
    static void Main()
    {
        Console.Write("Enter BMP file path (or press Enter for 'logo.bmp'): ");
        string bmpPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(bmpPath)) bmpPath = "logo.bmp";

        if (!File.Exists(bmpPath))
        {
            Console.WriteLine("Error: File not found.");
            return;
        }

        try
        {
            Bitmap bmp = new Bitmap(bmpPath);
            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                sw.WriteLine($"Color[,] logo = new Color[{bmp.Width}, {bmp.Height}];");

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color c = bmp.GetPixel(x, y);
                        string cosmosColor = ConvertToCosmosColor(c);
                        sw.WriteLine($"logo[{x}, {y}] = {cosmosColor};");
                    }
                }
            }
            Console.WriteLine("Done! Output saved to output.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static string ConvertToCosmosColor(Color c)
    {
        if (c.R == 255 && c.G == 255 && c.B == 255) return "Color.White";
        if (c.R == 0 && c.G == 0 && c.B == 0) return "Color.Black";
        if (c.R == 255 && c.G == 0 && c.B == 0) return "Color.Red";
        if (c.R == 0 && c.G == 255 && c.B == 0) return "Color.Green";
        if (c.R == 0 && c.G == 0 && c.B == 255) return "Color.Blue";
        if (c.R == 255 && c.G == 255 && c.B == 0) return "Color.Yellow";
        if (c.R == 0 && c.G == 255 && c.B == 255) return "Color.Cyan";
        if (c.R == 255 && c.G == 0 && c.B == 255) return "Color.Magenta";

        return "Color.White"; // Default fallback for unsupported colors
    }
}
