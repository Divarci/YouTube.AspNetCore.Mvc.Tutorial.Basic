﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouTube.AspNetCore.Tutorial.Basic.Context;

#nullable disable

namespace YouTube.AspNetCore.Tutorial.Basic.Migrations
{
    [DbContext(typeof(CustomContext))]
    [Migration("20240713150548_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Construction"
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.ControllerName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ControllerNames");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Authenticate"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dashboard"
                        },
                        new
                        {
                            Id = 3,
                            Name = "ControllerName"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Authenticated"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Category"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Domain"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Exception"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Product"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Role"
                        },
                        new
                        {
                            Id = 11,
                            Name = "UserRole"
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Domain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ControllerNameId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ControllerNameId");

                    b.HasIndex("RoleId");

                    b.ToTable("Domains");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ControllerNameId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            ControllerNameId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            ControllerNameId = 3,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 4,
                            ControllerNameId = 8,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 5,
                            ControllerNameId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 6,
                            ControllerNameId = 5,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 7,
                            ControllerNameId = 6,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 8,
                            ControllerNameId = 7,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 9,
                            ControllerNameId = 10,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 10,
                            ControllerNameId = 11,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 11,
                            ControllerNameId = 2,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 12,
                            ControllerNameId = 9,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 13,
                            ControllerNameId = 5,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 14,
                            ControllerNameId = 6,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 15,
                            ControllerNameId = 9,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "Brick",
                            Price = 2.5m,
                            Quantity = 15
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.ProductFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductFeatures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Colour = "Orange",
                            Height = 0.55000000000000004,
                            ProductId = 1,
                            Weight = 1.5
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Public"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Member"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "testadmin@gmail.com",
                            Fullname = "TestAdmin",
                            PasswordHash = "AQAAAAIAAYagAAAAEDZ67fm99um8gIy1DvA2jHPot3l5w37lO+VuId7v1rccwRUqij8rCz2GjAffK8LGfA=="
                        },
                        new
                        {
                            Id = 2,
                            Email = "testmember@gmail.com",
                            Fullname = "TestMember",
                            PasswordHash = "AQAAAAIAAYagAAAAEGLz4H894z/hrqr3jlzXm8zCVFWW4DsQSNvdiKZGV20GSyscLGM7WJ1ZffN7dS2fxA=="
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 3,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            RoleId = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            RoleId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            RoleId = 2,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Domain", b =>
                {
                    b.HasOne("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.ControllerName", "ControllerName")
                        .WithMany("Domains")
                        .HasForeignKey("ControllerNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Role", "Role")
                        .WithMany("Domain")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ControllerName");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Product", b =>
                {
                    b.HasOne("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.ProductFeature", b =>
                {
                    b.HasOne("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Product", "Product")
                        .WithOne("ProductFeature")
                        .HasForeignKey("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.ProductFeature", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.UserRole", b =>
                {
                    b.HasOne("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.ControllerName", b =>
                {
                    b.Navigation("Domains");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Product", b =>
                {
                    b.Navigation("ProductFeature");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.Role", b =>
                {
                    b.Navigation("Domain");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("YouTube.AspNetCore.Tutorial.Basic.Models.Entity.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}