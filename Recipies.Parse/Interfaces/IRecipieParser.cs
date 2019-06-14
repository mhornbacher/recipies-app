using Microsoft.EntityFrameworkCore;
using Recipies.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipies.Parse.Interfaces
{
    /// <summary>
    /// Represents a parser that can load recipies
    /// </summary>
    interface IRecipieParser
    {
        /// <summary>
        /// Get a list of file names and the category label for each one
        /// </summary>
        /// <returns>IDictionary<fileName, category></returns>
        IDictionary<string, string> GetFileNamesAndCategories();

        /// <summary>
        /// Parse a given filename for recipies
        /// </summary>
        /// <param name="FileName">name of file to parse</param>
        /// <returns>Enumerable of recipies</returns>
        Task<IEnumerable<Recipie>> ParseRecipieFile(string FileName);
    }
}
