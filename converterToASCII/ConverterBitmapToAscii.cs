using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace converterToASCII
{
    public class ConverterBitmapToAscii
    {
        private readonly char[] _asiiTable = { '.', ',', ':', '+', '*', '?', '%', '$', '#', '@' };
        private readonly char[] _asiiTableNegative = { '@', '#', '$', '%', '?', '*', '+', ':', ',', '.' };
        private readonly Bitmap _bitmap;

        public char[][] ConvertAsNegative()
        {
            return Convert(_asiiTableNegative);
        }

        public char[][] Convert()
        {
            return Convert(_asiiTable);
        }

        private char[][] Convert(char[] asciiTable)
        {
            var result = new char[_bitmap.Height][];

            for (int y = 0; y < _bitmap.Height; y++)
            {
                result[y] = new char[_bitmap.Width];

                for (int x = 0; x < _bitmap.Width; x++)
                {
                    int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length - 1);
                    result[y][x] = asciiTable[mapIndex];
                }
            }

            return result;
        }

        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }

        public ConverterBitmapToAscii(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }
    }
}
