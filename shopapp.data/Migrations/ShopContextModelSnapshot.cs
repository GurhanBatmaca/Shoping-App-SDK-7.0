﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shopapp.data.Concrete.EfCore;

#nullable disable

namespace shopapp.data.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("shopapp.entity.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("shopapp.entity.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("shopapp.entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kaşar",
                            Url = "kasar"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Eski Kaşar",
                            Url = "eski-kasar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Yeni Kaşar",
                            Url = "yeni-kasar"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Süzme Bal",
                            Url = "suzme-bal"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Petek Bal",
                            Url = "petek-bal"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Kara Kovan Bal",
                            Url = "kara-kovan-bal"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Çiçek Bal",
                            Url = "cicek-bal"
                        });
                });

            modelBuilder.Entity("shopapp.entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConversationId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("shopapp.entity.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("shopapp.entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHome")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPopular")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateAdded = new DateTime(2023, 9, 25, 17, 48, 47, 894, DateTimeKind.Local).AddTicks(408),
                            Description = "Yeni Kaşar",
                            ImageUrl = "1.jpg",
                            IsAproved = true,
                            IsHome = true,
                            IsPopular = true,
                            Name = "Yeni Kaşar",
                            Price = 250.0,
                            Url = "yeni-kasar"
                        },
                        new
                        {
                            Id = 2,
                            DateAdded = new DateTime(2023, 9, 25, 17, 48, 47, 894, DateTimeKind.Local).AddTicks(423),
                            Description = "Eski Kaşar",
                            ImageUrl = "2.jpg",
                            IsAproved = true,
                            IsHome = true,
                            IsPopular = false,
                            Name = "Eski Kaşar",
                            Price = 280.0,
                            Url = "eski-kasar"
                        },
                        new
                        {
                            Id = 3,
                            DateAdded = new DateTime(2023, 9, 25, 17, 48, 47, 894, DateTimeKind.Local).AddTicks(424),
                            Description = "Kara Kovan Balı",
                            ImageUrl = "3.jpg",
                            IsAproved = true,
                            IsHome = true,
                            IsPopular = true,
                            Name = "Kara Kovan Balı",
                            Price = 280.0,
                            Url = "kara-kovan-bali"
                        },
                        new
                        {
                            Id = 4,
                            DateAdded = new DateTime(2023, 9, 25, 17, 48, 47, 894, DateTimeKind.Local).AddTicks(426),
                            Description = "Petek Çiçek Balı",
                            ImageUrl = "4.jpg",
                            IsAproved = true,
                            IsHome = true,
                            IsPopular = false,
                            Name = "Petek Çiçek Balı",
                            Price = 280.0,
                            Url = "petek-cicek-bali"
                        },
                        new
                        {
                            Id = 5,
                            DateAdded = new DateTime(2023, 9, 25, 17, 48, 47, 894, DateTimeKind.Local).AddTicks(427),
                            Description = "Süzme Çiçek Balı",
                            ImageUrl = "5.jpg",
                            IsAproved = true,
                            IsHome = true,
                            IsPopular = true,
                            Name = "Süzme Çiçek Balı",
                            Price = 280.0,
                            Url = "suzme-cicek-bali"
                        });
                });

            modelBuilder.Entity("shopapp.entity.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 5
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 4
                        });
                });

            modelBuilder.Entity("shopapp.entity.CartItem", b =>
                {
                    b.HasOne("shopapp.entity.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shopapp.entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("shopapp.entity.OrderItem", b =>
                {
                    b.HasOne("shopapp.entity.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shopapp.entity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("shopapp.entity.ProductCategory", b =>
                {
                    b.HasOne("shopapp.entity.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shopapp.entity.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("shopapp.entity.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("shopapp.entity.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("shopapp.entity.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("shopapp.entity.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
