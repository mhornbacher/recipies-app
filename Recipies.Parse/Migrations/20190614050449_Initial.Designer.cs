﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipies.Parse._101JuiceRecipies;

namespace Recipies.Parse.Migrations
{
    [DbContext(typeof(JuiceRecipieContext))]
    [Migration("20190614050449_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Recipies.Core.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Recipies.Core.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<double>("Qty");

                    b.Property<string>("Unit");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Recipies.Core.Models.Nutrition", b =>
                {
                    b.Property<int>("NutritionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Calories");

                    b.Property<string>("Carbohydrate");

                    b.Property<string>("Fat");

                    b.Property<string>("Fiber");

                    b.Property<string>("Protein");

                    b.Property<string>("Sodium");

                    b.Property<string>("Sugars");

                    b.Property<string>("TransFat");

                    b.HasKey("NutritionId");

                    b.ToTable("Nutritions");
                });

            modelBuilder.Entity("Recipies.Core.Models.Recipie", b =>
                {
                    b.Property<int>("RecipieId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("ImgName");

                    b.Property<string>("Instructions");

                    b.Property<string>("Name");

                    b.Property<int?>("NutritionId");

                    b.HasKey("RecipieId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("NutritionId");

                    b.ToTable("Recipies");
                });

            modelBuilder.Entity("Recipies.Core.Models.RecipieIngredient", b =>
                {
                    b.Property<int>("RecipieIngredientId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IngredientForeignKey");

                    b.Property<int>("IngredientId");

                    b.Property<string>("Label");

                    b.Property<double>("Qty");

                    b.Property<int?>("RecipieForeignKey");

                    b.Property<string>("Unit");

                    b.HasKey("RecipieIngredientId");

                    b.HasIndex("IngredientForeignKey");

                    b.HasIndex("RecipieForeignKey");

                    b.ToTable("RecipieIngredients");
                });

            modelBuilder.Entity("Recipies.Core.Models.Recipie", b =>
                {
                    b.HasOne("Recipies.Core.Models.Category")
                        .WithMany("Recipies")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Recipies.Core.Models.Nutrition", "Nutrition")
                        .WithMany()
                        .HasForeignKey("NutritionId");
                });

            modelBuilder.Entity("Recipies.Core.Models.RecipieIngredient", b =>
                {
                    b.HasOne("Recipies.Core.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientForeignKey");

                    b.HasOne("Recipies.Core.Models.Recipie", "Recipie")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipieForeignKey");
                });
#pragma warning restore 612, 618
        }
    }
}