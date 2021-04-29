using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace converterToASCII
{
    class Program
    {
        private const double WIDTH_OFFSET = 1.7;
        private const int MAX_WIDTH = 474;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press enter to start...\n");

            while (true)
            {
                string cmd = Console.ReadLine();

                if (cmd == "exit")
                {
                    break;
                }

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    continue;
                }

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                ResizeBitmap(bitmap);
                bitmap.ToGrayScale();

                var converter = new ConverterBitmapToAscii(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                }


                var rowsNegative = converter.ConvertAsNegative();
                File.WriteAllLines("image.txt", rowsNegative.Select(r => new string(r)));  

                Console.SetCursorPosition(0, 0);
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {          
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            }
            return bitmap;
        }

    }
}