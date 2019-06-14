using Microsoft.EntityFrameworkCore;
using Recipies.Core.Models;
using Recipies.Parse._101JuiceRecipies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipies.Parse
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(
                "----------------------------------\n" +
                "--------- Recipie Parser ---------\n" +
                "----------------------------------\n");

            var juiceRecipieParser = new JuiceRecipieParser();

            using(var db = new JuiceRecipieContext())
            {
                await db.Database.MigrateAsync();

                await db.Categories.AddAsync(new Category
                {
                    Label = "A Category"
                });

                await db.SaveChangesAsync();

                var data = db.Categories.ToList();

                db.RemoveRange(data);

                await db.SaveChangesAsync();
            }
        }
    }
}
