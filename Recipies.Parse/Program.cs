using Microsoft.EntityFrameworkCore;
using Recipies.Core.Models;
using Recipies.Parse._101JuiceRecipies;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Recipies.Parse
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing Parser....");

            var juiceRecipieParser = new JuiceRecipieParser();

            Console.WriteLine("Deleting old database....");

            var path = JuiceRecipieContext.GetPath();

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var db = new JuiceRecipieContext())
            {
                Console.WriteLine("Migrating new database....");

                await db.Database.MigrateAsync();

                Console.WriteLine("Parsing data....");

                var catagory = await juiceRecipieParser
                    .ParseRecipieFile("part0006.html", "Green");

                Console.WriteLine("Adding parsed results....");

                await db.Categories.AddAsync(catagory);

                Console.WriteLine("Saving database...");

                await db.SaveChangesAsync();

                Console.WriteLine(
                    "----------------------------------------------\n" +
                    "--------------- Parsing Results --------------\n" +
                    "----------------------------------------------\n");

                Console.WriteLine($"Categories: {await db.Categories.CountAsync()}");
                Console.WriteLine($"Recipies: {await db.Recipies.CountAsync()}");
                Console.WriteLine($"Ingredients: {await db.Ingredients.CountAsync()}");
                Console.WriteLine($"Nutrition: {await db.Nutritions.CountAsync()}");

                Console.WriteLine("\n" +
                    "----------------------------------------------\n" +
                    "\n" +
                    $"Database file stored at: {path}");
            }
        }
    }
}
