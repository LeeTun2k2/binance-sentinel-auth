﻿using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace BinanceSential.Auth.Infrastructure.Data.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
  protected override void BuildModel(ModelBuilder modelBuilder)
  {
    modelBuilder
        .HasAnnotation("ProductVersion", "9.0.0")
        .HasAnnotation("Relational:MaxIdentifierLength", 63);

    NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

    modelBuilder.Entity("BinanceSential.Auth.Core.ContributorAggregate.Contributor", b =>
        {
          b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

          NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

          b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

          b.Property<int>("Status")
                    .HasColumnType("integer");

          b.HasKey("Id");

          b.ToTable("Contributor");
        });

    modelBuilder.Entity("BinanceSential.Auth.Core.UserAggregate.Role", b =>
        {
          b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

          b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text");

          b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

          b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

          b.HasKey("Id");

          b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex");

          b.ToTable("AspNetRoles", (string)null);
        });

    modelBuilder.Entity("BinanceSential.Auth.Core.UserAggregate.User", b =>
        {
          b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

          b.Property<int>("AccessFailedCount")
                    .HasColumnType("integer");

          b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text");

          b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

          b.Property<bool>("EmailConfirmed")
                    .HasColumnType("boolean");

          b.Property<bool>("LockoutEnabled")
                    .HasColumnType("boolean");

          b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("timestamp with time zone");

          b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

          b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

          b.Property<string>("PasswordHash")
                    .HasColumnType("text");

          b.Property<string>("PhoneNumber")
                    .HasColumnType("text");

          b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("boolean");

          b.Property<string>("SecurityStamp")
                    .HasColumnType("text");

          b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("boolean");

          b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

          b.HasKey("Id");

          b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

          b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex");

          b.ToTable("AspNetUsers", (string)null);
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
        {
          b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

          NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

          b.Property<string>("ClaimType")
                    .HasColumnType("text");

          b.Property<string>("ClaimValue")
                    .HasColumnType("text");

          b.Property<Guid>("RoleId")
                    .HasColumnType("uuid");

          b.HasKey("Id");

          b.HasIndex("RoleId");

          b.ToTable("AspNetRoleClaims", (string)null);
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
        {
          b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

          NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

          b.Property<string>("ClaimType")
                    .HasColumnType("text");

          b.Property<string>("ClaimValue")
                    .HasColumnType("text");

          b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

          b.HasKey("Id");

          b.HasIndex("UserId");

          b.ToTable("AspNetUserClaims", (string)null);
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
        {
          b.Property<string>("LoginProvider")
                    .HasColumnType("text");

          b.Property<string>("ProviderKey")
                    .HasColumnType("text");

          b.Property<string>("ProviderDisplayName")
                    .HasColumnType("text");

          b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

          b.HasKey("LoginProvider", "ProviderKey");

          b.HasIndex("UserId");

          b.ToTable("AspNetUserLogins", (string)null);
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
        {
          b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

          b.Property<Guid>("RoleId")
                    .HasColumnType("uuid");

          b.HasKey("UserId", "RoleId");

          b.HasIndex("RoleId");

          b.ToTable("AspNetUserRoles", (string)null);
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
        {
          b.Property<Guid>("UserId")
                    .HasColumnType("uuid");

          b.Property<string>("LoginProvider")
                    .HasColumnType("text");

          b.Property<string>("Name")
                    .HasColumnType("text");

          b.Property<string>("Value")
                    .HasColumnType("text");

          b.HasKey("UserId", "LoginProvider", "Name");

          b.ToTable("AspNetUserTokens", (string)null);
        });

    modelBuilder.Entity("BinanceSential.Auth.Core.ContributorAggregate.Contributor", b =>
        {
          b.OwnsOne("BinanceSential.Auth.Core.ContributorAggregate.PhoneNumber", "PhoneNumber", b1 =>
                    {
                      b1.Property<int>("ContributorId")
                                .HasColumnType("integer");

                      b1.Property<string>("CountryCode")
                                .IsRequired()
                                .HasColumnType("text");

                      b1.Property<string>("Extension")
                                .HasColumnType("text");

                      b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text");

                      b1.HasKey("ContributorId");

                      b1.ToTable("Contributor");

                      b1.WithOwner()
                                .HasForeignKey("ContributorId");
                    });

          b.Navigation("PhoneNumber");
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
        {
          b.HasOne("BinanceSential.Auth.Core.UserAggregate.Role", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
        {
          b.HasOne("BinanceSential.Auth.Core.UserAggregate.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
        {
          b.HasOne("BinanceSential.Auth.Core.UserAggregate.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
        {
          b.HasOne("BinanceSential.Auth.Core.UserAggregate.Role", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

          b.HasOne("BinanceSential.Auth.Core.UserAggregate.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        });

    modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
        {
          b.HasOne("BinanceSential.Auth.Core.UserAggregate.User", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        });
  }
}
