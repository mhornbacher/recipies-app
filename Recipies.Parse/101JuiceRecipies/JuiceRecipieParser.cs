using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Recipies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recipies.Parse._101JuiceRecipies
{
    class JuiceRecipieParser
    {
        private List<Ingredient> Ingredients;

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

        public async Task<Category> ParseRecipieFile(string fileName, string category)
        {
            var result = new Category
            {
                Label = category
            };

            var doc = new HtmlDocument();
            doc.Load("101JuiceRecipies\\files\\part0006.html");

            // toArray becuase HAP sucks. And sadly decent API's or editors are against the ideals of C#
            var filteredNodes = doc.DocumentNode.Descendants().ToArray()
                .Where(e => e.GetClasses().Count() > 0)
                .Where(e => ContainsDesiredClass(e))
                .ToList();

            var classesInOrder = filteredNodes
                .Select(e => e.GetAttributeValue("class", ""))
                .ToList();

            Recipie recipie = new Recipie();

            foreach( var node in filteredNodes )
            {
                var className = node.GetAttributeValue("class", "");
                var content = WebUtility.HtmlDecode( node.InnerText );

                switch (className)
                {
                    case "head-g":
                        
                        if ( !string.IsNullOrEmpty(recipie.Name) )
                        {
                            result.Recipies.Add(recipie);
                        }

                        recipie = new Recipie()
                        {
                            Name = content
                        };

                        break;

                    case "extract":

                        recipie.Description = content;
                        break;

                    case "center1":
                        // TODO get image name
                        break;

                    case "ing-hang":
                    case "ing-hang1":
                        // TODO: Parse instructions and recipies
                        break;

                    case "item":
                        // TODO: Parse nutritional values
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        private bool ContainsDesiredClass(HtmlNode node)
        {
            var classes = node.GetClasses();

            return classes.Contains("head-g")
                || classes.Contains("extract")
                || classes.Contains("center1")
                || classes.Contains("ing-hang")
                || classes.Contains("ing-hang1")
                || classes.Contains("item");
        }
    }
}
