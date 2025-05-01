using System.Text.RegularExpressions;

namespace ColorConverter
{
    public class ColorConverterDll
    {
        // RGB → HEX
        public static string RgbToHex(byte r, byte g, byte b)
        {
            return $"#{r:X2}{g:X2}{b:X2}";
        }

        // HEX → RGB
        public static (byte r, byte g, byte b) HexToRgb(string hex)
        {
            hex = hex.Replace("#", "");

            if (hex.Length == 3)
                hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}";

            if (hex.Length != 6)
                throw new ArgumentException("Некорректный HEX-формат");

            return (
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16)
            );
        }

        // RGB → HSL
        public static (double h, double s, double l) RgbToHsl(byte r, byte g, byte b)
        {
            double rd = r / 255.0;
            double gd = g / 255.0;
            double bd = b / 255.0;

            double max = Math.Max(rd, Math.Max(gd, bd));
            double min = Math.Min(rd, Math.Min(gd, bd));
            double delta = max - min;

            double h = 0, s, l = (max + min) / 2;

            if (delta != 0)
            {
                s = l > 0.5 ? delta / (2 - max - min) : delta / (max + min);

                if (max == rd) h = (gd - bd) / delta + (gd < bd ? 6 : 0);
                else if (max == gd) h = (bd - rd) / delta + 2;
                else h = (rd - gd) / delta + 4;

                h *= 60;
            }
            else
            {
                s = 0;
            }

            return (Math.Round(h, 2), Math.Round(s, 2), Math.Round(l, 2));
        }

        // HSL → RGB
        public static (byte r, byte g, byte b) HslToRgb(double h, double s, double l)
        {
            if (s == 0)
            {
                byte val = (byte)(l * 255);
                return (val, val, val);
            }

            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;

            double HueToRgb(double t)
            {
                if (t < 0) t += 1;
                if (t > 1) t -= 1;
                if (t < 1 / 6.0) return p + (q - p) * 6 * t;
                if (t < 1 / 2.0) return q;
                if (t < 2 / 3.0) return p + (q - p) * (2 / 3.0 - t) * 6;
                return p;
            }

            return (
                (byte)(HueToRgb(h / 360 + 1 / 3.0) * 255),
                (byte)(HueToRgb(h / 360) * 255),
                (byte)(HueToRgb(h / 360 - 1 / 3.0) * 255)
            );
        }

    }
}
