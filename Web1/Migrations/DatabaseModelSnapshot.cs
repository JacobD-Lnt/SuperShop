﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web1;

namespace Web1.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Web1.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<string>("ToUsername")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ToUsername");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Web1.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("AmountSpent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateBought")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("Web1.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Web1.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Web1.Message", b =>
                {
                    b.HasOne("Web1.User", "To")
                        .WithMany("Inbox")
                        .HasForeignKey("ToUsername");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Web1.Receipt", b =>
                {
                    b.HasOne("Web1.User", "User")
                        .WithMany("Receipts")
                        .HasForeignKey("Username");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web1.ShoppingCart", b =>
                {
                    b.HasOne("Web1.User", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("Username");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web1.User", b =>
                {
                    b.Navigation("Inbox");

                    b.Navigation("Receipts");

                    b.Navigation("ShoppingCarts");
                });
#pragma warning restore 612, 618
        }
    }
}