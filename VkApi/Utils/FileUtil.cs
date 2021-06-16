using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace VkApi
{
    public class FileUtil
    {
        public static bool FileEquals(byte[] file1, byte[] file2)
        {
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static bool CompareImages(Image img1, Image img2)
        {
            Bitmap expectedPhoto = new Bitmap(img1);
            Bitmap actualPhoto = new Bitmap(img2);
            float diff = 0;
            float result = 0;
            int precision = 7;

            if (expectedPhoto.Width == actualPhoto.Width && expectedPhoto.Height == actualPhoto.Height)
            {
                for (int i = 0; i < expectedPhoto.Width; i++)
                {
                    for (int j = 0; j < actualPhoto.Height; j++)
                    {
                        Color expectedPixel = expectedPhoto.GetPixel(i, j);
                        Color actualPixel = actualPhoto.GetPixel(i, j);
                        diff += Math.Abs(expectedPixel.R - actualPixel.R);
                        diff += Math.Abs(expectedPixel.G - actualPixel.G);
                        diff += Math.Abs(expectedPixel.B - actualPixel.B);
                    }
                }
                result = diff.Equals(float.Parse("0")) ? 100 : 100 * (diff / 255) / (expectedPhoto.Width * expectedPhoto.Height * 3);
            }
            else
            {
                result = 0;
            }
            return Math.Round(result, precision).Equals(Math.Round(double.Parse("100"), precision));
        }
    }
}