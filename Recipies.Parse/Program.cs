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
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Fatal Error: {e.ToString()}");
                    return;
                }
            }

            using (var db = new JuiceRecipieContext())
            {
                Console.WriteLine("Migrating new database....");

                await db.Database.MigrateAsync();

                Console.WriteLine("Parsing data....");

                var categories = await juiceRecipieParser.ParseRecipies();

                Console.WriteLine("Adding parsed results....");

                await db.Categories.AddRangeAsync(categories);

                Console.WriteLine("Saving database...");

                await db.SaveChangesAsync();

                Console.WriteLine(
                    "----------------------------------------------\n" +
                    "--------------- Parsing Results --------------\n" +
                    "----------------------------------------------\n");

                Console.WriteLine($"Categories: {await db.Categories.CountAsync()}");
                Console.WriteLine($"Recipies: {await db.Recipies.CountAsync()}");
                Console.WriteLine($"Ingredients: {await db.Ingredients.CountAsync()}");

                Console.WriteLine("\n" +
                    "----------------------------------------------\n" +
                    "\n" +
                    $"Database file stored at: {path}");
            }
        }
    }
}
