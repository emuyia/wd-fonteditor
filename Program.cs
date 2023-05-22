using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using static System.Net.WebRequestMethods;

namespace wdfe
{
    public class FontParameters
    {
        // Byte array to store font and character data
        public byte[] FontBytes { get; set; }
        public byte[] CharBytes { get; set; }

        // Bitmap object for character
        public Bitmap CharBMP { get; set; }

        // Store last loaded font and directory
        public string LastLoadedFont { get; set; }
        public string LastDirUsed { get; set; }

        // Variables to store pixel width, header trim, trim offset and character byte span
        public int PixelWidth { get; set; }
        public int HeaderTrim { get; set; }
        public int TrimOffset { get; set; }
        public int CharByteSpan { get; set; }

        // List to store untouched font bytes and character width table
        public List<byte> FontBytesUntouched { get; set; } = new List<byte>();
        public List<byte> CharWidthTable { get; set; } = new List<byte>();

        // Variables to store padding related values
        public int PadPixelCount { get; set; }
        public int PadByteCount { get; set; }
        public int BMPStride { get; set; }
    }

    // Main method entry point
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Run the FontEditor form on application start
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FontEditor());
        }
    }

    // Utils class contains useful methods to be used across the application
    public static class Utils
    {
        // Method to repeat a given action a specified number of times
        public static void RepeatAction(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        // Method to round up a number to the nearest multiple
        public static int RoundUp(int numToRound, int multiple)
        {
            if (multiple == 0)
                return numToRound;

            int remainder = numToRound % multiple;
            if (remainder == 0)
                return numToRound;

            return numToRound + multiple - remainder;
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string ShowSaveFileDialog(string filter, string defaultFileName)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = filter,
                FileName = defaultFileName
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }

        public static string ShowOpenFileDialog(string filter, string title)
        {
            OpenFileDialog openSymbolDialog = new()
            {
                Title = title,
                Filter = filter,
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            return openSymbolDialog.ShowDialog() == DialogResult.OK ? openSymbolDialog.FileName : null;
        }
    }

    // Class to extend Bitmap functionality
    public static class BitmapExtensions
    {
        // Crop the bitmap at the given rectangle
        public static Bitmap CropAtRect(this Bitmap b, Rectangle r)
        {
            Bitmap nb = new(r.Width, r.Height);

            using Graphics g = Graphics.FromImage(nb);

            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        // Resize the bitmap to given dimensions
        public static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }

        private const int PaletteSize = 16;

        // Convert byte array to 4bpp image
        public static Bitmap ByteTo4bppImage(byte[] blob, int imagePixelWidth, int imagePixelHeight, int imageStride)
        {
            // unsafe keyword allows pointer arithmetic
            unsafe
            {
                // fixed keyword pins the location of blob in memory so GC won't move it while we're using it
                fixed (byte* ptr = blob)
                {
                    Bitmap imageBMP = new(imagePixelWidth, imagePixelHeight, imageStride, PixelFormat.Format4bppIndexed, new IntPtr(ptr));

                    ColorPalette palette = imageBMP.Palette;
                    Color[] entries = palette.Entries;

                    for (int i = 0; i < PaletteSize; i++)
                    {
                        entries[i] = Color.FromArgb(i * PaletteSize, i * PaletteSize, i * PaletteSize);
                    }

                    imageBMP.Palette = palette;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    return imageBMP;
                }
            }
        }

        // Convert Bitmap to byte array
        public static byte[] BmpToByteArray(string bmpInput)
        {
            unsafe
            {
                using var bmp = new Bitmap(bmpInput);

                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);

                var bmpByteLength = bmpData.Stride * bmpData.Height;
                byte[] rawBytes = new byte[bmpByteLength];
                Marshal.Copy(bmpData.Scan0, rawBytes, 0, bmpByteLength);
                bmp.UnlockBits(bmpData);
                return rawBytes;
            }
        }

        // Prepare Bitmap for character box (for display purposes)
        public static Bitmap PrepareBMP4CharBox(Bitmap CharBMP, int PadPixelCount, int PixelWidth)
        {
            // Use new bitmap that isn't 4bpp so that it can be displayed
            using Bitmap tempBitmap = new(CharBMP.Width, CharBMP.Height);
            
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                // Render 4bpp bmp to non-4bpp temporary bitmap
                g.DrawImage(CharBMP, 0, 0); 
            }

            Rectangle cropRect = new(PadPixelCount, 0, PixelWidth, PixelWidth);
            Bitmap padTrim = BitmapExtensions.CropAtRect(tempBitmap, cropRect);

            return BitmapExtensions.ResizeBitmap(padTrim, padTrim.Width * 4, padTrim.Height * 4);
            
        }

        public static void SaveBitmap(Bitmap bitmap, string filePath)
        {
            using FileStream fs = new(filePath, FileMode.Create, FileAccess.ReadWrite);
            using MemoryStream memory = new();

            bitmap.Save(memory, ImageFormat.Bmp);
            byte[] bytes = memory.ToArray();
            fs.Write(bytes, 0, bytes.Length);
        }

    }
}
