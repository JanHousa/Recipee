﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipee.Models;

#nullable disable

namespace Recipee.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RecipeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = "200g",
                            Name = "Čokoláda",
                            RecipeId = 1
                        });
                });

            modelBuilder.Entity("Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AverageRating")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AverageRating = 4.5,
                            CreatedDate = new DateTime(2024, 4, 15, 21, 16, 44, 518, DateTimeKind.Local).AddTicks(4396),
                            Description = "Bohatý čokoládový dort s třemi vrstvami.",
                            ImageUrl = "url_k_obrazku_dortu",
                            Instructions = "Smíchejte suroviny a pečte na 180°C 50 minut.",
                            Title = "Čokoládový dort"
                        },
                        new
                        {
                            Id = 2,
                            AverageRating = 4.0,
                            CreatedDate = new DateTime(2024, 4, 15, 21, 16, 44, 518, DateTimeKind.Local).AddTicks(4434),
                            Description = "Klasický Caesar salát s kuřecím masem.",
                            ImageUrl = "https://receptypanicuby.cz/wp-content/uploads/2020/08/caesar-salat-recept-5.jpg",
                            Instructions = "Smíchejte a podávejte čerstvé.",
                            Title = "Caesar salát"
                        });
                });

            modelBuilder.Entity("Ingredient", b =>
                {
                    b.HasOne("Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
