﻿// <auto-generated />
using DungeonsAndDragons.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DungeonsAndDragons.Database.Migrations
{
    [DbContext(typeof(GameDbContext))]
    partial class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Armor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArmorClassBase")
                        .HasColumnType("integer");

                    b.Property<int>("ArmorType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Armors");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Monster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("RaceId");

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("RaceId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DamageType")
                        .HasColumnType("integer");

                    b.Property<int>("Proficiency")
                        .HasColumnType("integer");

                    b.Property<int>("Property")
                        .HasColumnType("integer");

                    b.Property<int>("RangeType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Class", b =>
                {
                    b.OwnsOne("DungeonsAndDragons.Database.Model.GameDice", "HitsGameDice", b1 =>
                        {
                            b1.Property<int>("ClassId")
                                .HasColumnType("integer");

                            b1.Property<int>("DiceAmount")
                                .HasColumnType("integer");

                            b1.Property<int>("MaxValue")
                                .HasColumnType("integer");

                            b1.HasKey("ClassId");

                            b1.ToTable("Classes");

                            b1.WithOwner()
                                .HasForeignKey("ClassId");
                        });

                    b.Navigation("HitsGameDice")
                        .IsRequired();
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Monster", b =>
                {
                    b.HasOne("DungeonsAndDragons.Database.Model.Class", "Type")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DungeonsAndDragons.Database.Model.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DungeonsAndDragons.Database.Model.Characteristics", "Characteristics", b1 =>
                        {
                            b1.Property<int>("MonsterId")
                                .HasColumnType("integer");

                            b1.Property<int>("Charisma")
                                .HasColumnType("integer");

                            b1.Property<int>("Constitution")
                                .HasColumnType("integer");

                            b1.Property<int>("Dexterity")
                                .HasColumnType("integer");

                            b1.Property<int>("Intelligence")
                                .HasColumnType("integer");

                            b1.Property<int>("Strength")
                                .HasColumnType("integer");

                            b1.Property<int>("Wisdom")
                                .HasColumnType("integer");

                            b1.HasKey("MonsterId");

                            b1.ToTable("Monsters");

                            b1.WithOwner()
                                .HasForeignKey("MonsterId");
                        });

                    b.Navigation("Characteristics")
                        .IsRequired();

                    b.Navigation("Race");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Player", b =>
                {
                    b.HasOne("DungeonsAndDragons.Database.Model.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DungeonsAndDragons.Database.Model.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Race");
                });

            modelBuilder.Entity("DungeonsAndDragons.Database.Model.Weapon", b =>
                {
                    b.OwnsOne("DungeonsAndDragons.Database.Model.GameDice", "GameDice", b1 =>
                        {
                            b1.Property<int>("WeaponId")
                                .HasColumnType("integer");

                            b1.Property<int>("DiceAmount")
                                .HasColumnType("integer");

                            b1.Property<int>("MaxValue")
                                .HasColumnType("integer");

                            b1.HasKey("WeaponId");

                            b1.ToTable("Weapons");

                            b1.WithOwner()
                                .HasForeignKey("WeaponId");
                        });

                    b.Navigation("GameDice")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
