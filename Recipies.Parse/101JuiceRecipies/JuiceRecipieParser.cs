using HtmlAgilityPack;
using Recipies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recipies.Parse._101JuiceRecipies
{
    class JuiceRecipieParser
    {
        private Dictionary<string, Ingredient> NameIngredientMap
            = new Dictionary<string, Ingredient>();

        private Regex instructionsPattern = new Regex(@"([0-9])+\) (.+)");
        private Regex ingredientPattern = new Regex(@"— ([0-9½⅓¼⅔ ]+)(.+)");

        public async Task<IEnumerable<Category>> ParseRecipies()
        {
            var tasks = new List<Task<Category>>();
            var fileNamesAndCategories = new Dictionary<string, string>
            {
                { "part0006.html", "Green" },
                { "part0007.html", "Yellow" },
                { "part0008.html", "Orange" },
                { "part0009.html", "Red" },
                { "part0010.html", "Purple" },
            };

            foreach(var kvp in fileNamesAndCategories)
            {
                tasks.Add(ParseRecipieFile(kvp.Key, kvp.Value));
            }

            return await Task.WhenAll(tasks);
        }

        private async Task<Category> ParseRecipieFile(string fileName, string category)
        {
            var result = new Category
            {
                Name = category
            };

            var doc = new HtmlDocument();
            doc.Load($"101JuiceRecipies\\files\\{fileName}");

            // toArray becuase HAP sucks. And sadly decent API's or editors are against the ideals of C#
            var filteredNodes = doc.DocumentNode.Descendants().ToArray()
                .Where(e => e.GetClasses().Count() > 0)
                .Where(e => ContainsDesiredClass(e))
                .ToList();

            var classesInOrder = filteredNodes
                .Select(e => e.GetAttributeValue("class", ""))
                .ToList();

            Recipie recipie = null;

            foreach (var node in filteredNodes)
            {
                var className = node.GetAttributeValue("class", "");
                var content = WebUtility.HtmlDecode( node.InnerText );

                if ( className.StartsWith("head-") )
                {
                    if (recipie != null)
                    {
                        result.Recipies.Add(recipie);
                    }

                    recipie = new Recipie()
                    {
                        Name = content
                    };
                }
                else if (className.Equals("extract"))
                {
                    recipie.Description = content;
                }
                else if (className.Equals("center1"))
                {
                    recipie.ImgName = node
                        .FirstChild
                        .GetAttributeValue("src", "")
                        .Replace("../images/", "");
                }
                else if (className.StartsWith("ing-hang")
                    && ingredientPattern.IsMatch(content))
                {
                    var match = ingredientPattern.Match(content);
                    var name = match.Groups[2].ToString().Trim();
                    var qty = match.Groups[1].ToString().Trim().Replace(" ", "");

                    recipie.Ingredients.Add(new RecipieIngredient()
                    {
                        Qty = GetQty(qty),
                        Ingredient = GetIngredient(name),
                        Optional = name.Contains("(optional)")
                    });
                }
                else if (className.StartsWith("ing-hang")
                    && instructionsPattern.IsMatch(content))
                {
                    var instruction = instructionsPattern
                        .Match(content)
                        .Groups[2]
                        .ToString()
                        .Trim();

                    recipie.Instructions.Add(instruction);
                }
                else if (className.Equals("r1"))
                {
                    IconTypeParser.ParseImgSrc(node, ref recipie);
                }
            }

            return result;
        }

        private bool ContainsDesiredClass(HtmlNode node)
        {
            var className = node.GetAttributeValue("class", "");

            return className.StartsWith("head-")
                || className.Equals("extract")
                || className.Equals("center1")
                || className.StartsWith("ing-hang")
                || className.Equals("r1");
        }

        private double GetQty(string qtyString)
        {
            qtyString = qtyString
                .Replace("⅔", ".66")
                .Replace("½", ".50")
                .Replace("⅓", ".33")
                .Replace("¼", ".25");
            return double.Parse(qtyString);
        }

        private Ingredient GetIngredient(string name)
        {
            Ingredient ingredient;

            Unit unit = Unit.STANDARD;

            name = name.Replace("(optional)", "");

            // measured in inches
            if (name.StartsWith('"'))
            {
                unit = Unit.CM;
                name = Regex.Replace(name, @"""? ?\/? ?\/ ?[0-9.]+?cm ", "");
            }
            else if (name.StartsWith("cup"))
            {
                unit = Unit.CUP;
                name = Regex.Replace(name, @"cups? ?\/ ?[0-9]+?g ", "");
            }
            else if (name.StartsWith("tsp"))
            {
                unit = Unit.TSP;
                name = Regex.Replace(name, @"tsp ?\/ ?[0-9]+?ml ", "");
            }

            name = name.Trim();

            if (NameIngredientMap.TryGetValue(name, out ingredient))
            {
                return ingredient;
            }

            ingredient = new Ingredient()
            {
                Name = name,
                Unit = unit
            };

            NameIngredientMap.Add(name, ingredient);
            return ingredient;
        }
    }
}
