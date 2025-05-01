using System.Drawing;
using ColorConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit_tests
{
    [TestFixture]
    public class ColorConverterTests
    {
        // Тесты для RgbToHex
        [Test]
        public void RgbToHex_ConvertsCorrectly()
        {
            var result = ColorConverterDll.RgbToHex(255, 0, 128);
            Assert.AreEqual("#FF0080", result);
        }

        // Тесты для HexToRgb
        [Test]
        public void HexToRgb_ConvertsStandardFormat()
        {
            var (r, g, b) = ColorConverterDll.HexToRgb("#FF8000");
            Assert.AreEqual((255, 128, 0), (r, g, b));
        }

        [Test]
        public void HexToRgb_ConvertsShortFormat()
        {
            var (r, g, b) = ColorConverterDll.HexToRgb("#F80");
            Assert.AreEqual((255, 136, 0), (r, g, b));
        }

        // Тесты для RgbToHsl
        [Test]
        public void RgbToHsl_ConvertsPrimaryColors()
        {
            var (h, s, l) = ColorConverterDll.RgbToHsl(0, 255, 0);
            Assert.AreEqual((120, 1.0, 0.5), (h, s, l));
        }

        // Тесты для HslToRgb
        [Test]
        public void HslToRgb_ConvertsPrimaryColors()
        {
            var (r, g, b) = ColorConverterDll.HslToRgb(240, 1.0, 0.5);
            Assert.AreEqual((0, 0, 255), (r, g, b));
        }

        // Интеграционные тесты
        [Test]
        public void Conversion_RoundTrip_RgbHex()
        {
            var original = (100, 150, 200);
            var hex = ColorConverterDll.RgbToHex(
                (byte)original.Item1,
                (byte)original.Item2,
                (byte)original.Item3); ;
            var converted = ColorConverterDll.HexToRgb(hex);
            Assert.AreEqual(original, converted);
        }
    }
}