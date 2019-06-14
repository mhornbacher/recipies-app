using Microsoft.EntityFrameworkCore;
using Recipies.Core.Models;
using Recipies.Parse.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recipies.Parse._101JuiceRecipies
{
    class JuiceRecipieParser : IRecipieParser
    {
        private JuiceRecipieContext GetDbContext()
        {
            return new JuiceRecipieContext();
        }

        /// <summary>
        /// Returns hardcoded list of filenames and their categories
        /// </summary>
        public IDictionary<string, string> GetFileNamesAndCategories()
        {
            return new Dictionary<string, string>
            {
                { "part0006.html", "Green" },
                { "part0007.html", "Yellow" },
                { "part0008.html", "Orange" },
                { "part0009.html", "Red" },
                { "part0010.html", "Purple" },
            };
        }

        /// <summary>
        /// Parses HTML file from book for recipies asyncronusly
        /// </summary>
        /// <param name="FileName">Name of html file from epub</param>
        public Task<IEnumerable<Recipie>> ParseRecipieFile(string FileName)
        {
            throw new NotImplementedException();
        }
    }
}
