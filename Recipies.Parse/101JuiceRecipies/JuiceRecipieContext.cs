using Microsoft.EntityFrameworkCore;
using Recipies.Core.Contexts;
using System.IO;

namespace Recipies.Parse._101JuiceRecipies
{
    class JuiceRecipieContext : IRecipieContext
    {
        private string _fileName = "101JuiceRecipies";

        public JuiceRecipieContext() { }

        public JuiceRecipieContext(string fileName)
        {
            _fileName = fileName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = GetPath(_fileName);
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        public static string GetPath( string fileName = "101JuiceRecipies" )
        {
            return $"{Path.Combine(Directory.GetCurrentDirectory(), "101JuiceRecipies", fileName)}.sqlite";
        }
    }
}
