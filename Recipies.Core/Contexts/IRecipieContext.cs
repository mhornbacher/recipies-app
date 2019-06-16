using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipies.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Recipies.Core.Contexts
{
    public abstract class IRecipieContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipie> Recipies { get; set; }
        public DbSet<RecipieIngredient> RecipieIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            // store lists of strings seperated by semicolon
            var splitStringConverter = new ValueConverter<List<string>, string>(
                v => string.Join(";", v),
                v => v.Split(new[] { ';' }).ToList() );

            // store instructions seperated by a semicolon
            builder.Entity<Recipie>()
                .Property(nameof(Recipie.Instructions))
                .HasConversion(splitStringConverter);

            // ingredient labels are unique
            builder.Entity<Ingredient>()
                .HasIndex(i => i.Label)
                .IsUnique();

            // category labels are unique
            builder.Entity<Category>()
                .HasIndex(c => c.Label)
                .IsUnique();
        }
    }
}
