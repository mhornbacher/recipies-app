using HtmlAgilityPack;
using Recipies.Core.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Recipies.Parse._101JuiceRecipies
{
    class IconTypeParser
    {
        private static List<int> RebootIds = new List<int>()
        {
            09, 15, 22, 63, 66, 80, 83, 111, 120, 129, 137, 143,
        };
        private static List<int> QuickImgIds = new List<int>()
        {
            06, 32, 40, 66, 68, 83, 86, 108, 113, 129,
        };
        private static List<int> WorkoutImgIds = new List<int>()
        {
            07, 22, 29, 105, 108, 120, 124, 137,
        };
        private static List<int> FruitImgIds = new List<int>()
        {
            08, 40, 58, 76, 86, 92, 116, 124, 147
        };

        private static Regex regex = new Regex(@"([0-9]+).jpeg");


        public static void ParseImgSrc(HtmlNode node, ref Recipie recipie)
        {
            var path = node.FirstChild.GetAttributeValue("src", "");
            var keyString = regex.Match(path).Groups[1].ToString();

            if ( string.IsNullOrEmpty( keyString ) )
            {
                return;
            }

            var key = int.Parse(keyString);
            Parse(key, ref recipie);
        }

        public static void Parse(int key, ref Recipie recipie)
        {
            if (RebootIds.Contains(key))
            {
                recipie.Reboot = true;
            }

            if (QuickImgIds.Contains(key))
            {
                recipie.Quick = true;
            }

            if (WorkoutImgIds.Contains(key))
            {
                recipie.PostWorkout = true;
            }

            if (FruitImgIds.Contains(key))
            {
                recipie.Fruit = true;
            }
        }
    }
}
