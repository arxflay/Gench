using System;
using System.Text;
namespace Gench.Business
{
    public class PartColor
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        private string ByteToHexString(byte color)
        {
            string str = color.ToString("X");

            if (str.Length == 0)
            {
                str = "00";
            }
            else if (str.Length == 1)
            {
                str = "0" + str;
            }

            return str;
        }

        private string GetRGB()
        {
            return "#" + ByteToHexString(R) + ByteToHexString(G) + ByteToHexString(B);
        }


        public PartColor(byte r = 0xff, byte g = 0xff, byte b = 0xff)
        {
            R = r;
            G = g;
            B = b;
        }
        
        public void SetPartColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public override string ToString()
        {
            return (GetRGB()).ToLower();
        }

    }
}