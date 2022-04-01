﻿// <auto-generated />
using System;
using BusinessHours.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessHours.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessHours.Domain.Entities.BusinessHoursRule", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.Department", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("RuleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique()
                        .HasFilter("[ExternalId] IS NOT NULL");

                    b.HasIndex("RuleId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.Holiday", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AllDay")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Finish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.RuleHoliday", b =>
                {
                    b.Property<string>("RuleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HolidayId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RuleId", "HolidayId");

                    b.HasIndex("HolidayId");

                    b.ToTable("RulesHolidays");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.WorkHours", b =>
                {
                    b.Property<string>("RuleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Finish")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("Open")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Start")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("RuleId", "Day");

                    b.ToTable("WorkHours");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.Department", b =>
                {
                    b.HasOne("BusinessHours.Domain.Entities.BusinessHoursRule", "Rule")
                        .WithMany("Departments")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.RuleHoliday", b =>
                {
                    b.HasOne("BusinessHours.Domain.Entities.Holiday", "Holiday")
                        .WithMany("Rules")
                        .HasForeignKey("HolidayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessHours.Domain.Entities.BusinessHoursRule", "Rule")
                        .WithMany("Holidays")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Holiday");

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.WorkHours", b =>
                {
                    b.HasOne("BusinessHours.Domain.Entities.BusinessHoursRule", "Rule")
                        .WithMany("WorkHours")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.BusinessHoursRule", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Holidays");

                    b.Navigation("WorkHours");
                });

            modelBuilder.Entity("BusinessHours.Domain.Entities.Holiday", b =>
                {
                    b.Navigation("Rules");
                });
#pragma warning restore 612, 618
        }
    }
}
