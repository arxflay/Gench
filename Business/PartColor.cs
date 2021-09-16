using System;
using System.Text;
namespace Gench.Business
{
    public class PartColor
    {
        public byte R{ get; set; }
        public byte G{ get; set; }
        public byte B{ get; set; }
        public byte A{ get; set; }

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

        public PartColor()
        {
        }
        
        public void SetPartColor(byte r, byte g, byte b, byte a = 0xff)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override string ToString()
        {
            return "#" + (ByteToHexString(R) + ByteToHexString(G) + ByteToHexString(B)).ToLower();
        }

        public static PartColor StringToPartColor(string color)
        {
            return null;
        }

    }
}