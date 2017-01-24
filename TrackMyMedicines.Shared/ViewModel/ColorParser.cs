using System;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ReactiveUI;

namespace TrackMyMedicine.ViewModel
{
    /// <summary>
    ///     Parses #aarrggbb strings into <see cref="Color" />s.
    /// </summary>
    public static class ColorParser
    {
        private class NamedBrush
        {
            public string Name { get; set; }
            public SolidColorBrush Brush { get; set; }
        }

        private static readonly ReactiveList<NamedBrush> ExisitingColors =
            new ReactiveList<NamedBrush>(from x in
                new[]
                {
                    Colors.Red, Colors.OrangeRed,
                    Colors.Orange, Colors.Gold,
                    Colors.Yellow, Colors.YellowGreen,
                    Colors.Green, Colors.DarkSeaGreen,
                    Colors.LightSeaGreen, Colors.DarkCyan,
                    Colors.DeepSkyBlue, Colors.DodgerBlue,
                    Colors.Blue, Colors.Indigo,
                    Colors.Purple, Colors.Violet
                }
                select new NamedBrush { Name = x.ToString() , Brush = new SolidColorBrush(x)});

        public static IReactiveDerivedList<SolidColorBrush> AllBrushes { get; } = ExisitingColors.CreateDerivedCollection(x => x.Brush);

     
        internal static SolidColorBrush FromRGBString(string colour)
        {
            if (String.IsNullOrEmpty(colour))
            {
                throw new ArgumentException("Can't parse an empty string.");
            }

            var existing = ExisitingColors.FirstOrDefault(x => x.Name == colour);
            if (existing != null)
            {
                return existing.Brush;
            }

            var colorPattern =
                new Regex(
                    "#(?<alpha>[0-9a-fA-F]{2})(?<red>[0-9a-fA-F]{2})(?<green>[0-9a-fA-F]{2})(?<blue>[0-9a-fA-F]{2})");
            var match = colorPattern.Match(colour);
            if (match.Success == false)
            {
                throw new ArgumentException(colour + " is not valid. it should look like \"#aarrggbb\".");
            }

            var a = Hex.Parse(match.Groups["alpha"].Value);
            var r = Hex.Parse(match.Groups["red"].Value);
            var g = Hex.Parse(match.Groups["green"].Value);
            var b = Hex.Parse(match.Groups["blue"].Value);
            var result = new SolidColorBrush(Color.FromArgb((byte) a, (byte) r, (byte) g, (byte) b));
            ExisitingColors.Add(new NamedBrush { Name= colour, Brush = result});
            return result;
        }

        public static SolidColorBrush TintFrom(Color color, int tintAlpha = TintAlpha)
        {
            var colorString = color.ToString();
            return FromRGBString("#" + tintAlpha.ToHex() + colorString.Substring(3));
        }

        public static SolidColorBrush From(Color color)
        {
            return FromRGBString(color.ToString());
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public const int TintAlpha = 64;
    }

    /// <summary>
    ///     Parses hex sstrings to <see cref="int" />s and vice versa.
    /// </summary>
    public static class Hex
    {
        private static readonly string[] HexChars =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c",
            "d", "e", "f"
        };

        public static int Parse(string hex)
        {
            return 16*Parse(hex[0]) + Parse(hex[1]);
        }

        public static string ToHex(this int value)
        {
            return HexChars[value/16] + HexChars[value % 16];
        }

        public static int Parse(char hex)
        {
            if (hex >= '0'
                && hex <= '9')
            {
                return hex - '0';
            }

            if (hex >= 'a'
                && hex <= 'f')
            {
                return hex - 'a' + 10;
            }

            if (hex >= 'A'
                && hex <= 'F')
            {
                return hex - 'A' + 10;
            }

            throw new ArgumentException(string.Format("'{0}' is not a valid hexadecimal character", hex));
        }
    }
}