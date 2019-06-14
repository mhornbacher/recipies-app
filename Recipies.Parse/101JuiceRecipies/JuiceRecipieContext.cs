using Microsoft.EntityFrameworkCore;
using Recipies.Core.Contexts;
using Recipies.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            var path = $"{Path.Combine(Directory.GetCurrentDirectory(), _fileName)}.sqlite";
            optionsBuilder.UseSqlite($"Filename={path}");
        }
    }
}
