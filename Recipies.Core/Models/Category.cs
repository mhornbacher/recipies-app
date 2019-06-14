using System.Collections.Generic;

namespace Recipies.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Label { get; set; }
        public List<Recipie> Recipies { get; set; }
    }
}
