﻿// <auto-generated />
using System;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicManager.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ClinicManagerDbContext))]
    partial class ClinicManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ClinicManager.Domain.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("MedicalSpecialty")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("ClinicManager.Domain.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("ClinicManager.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Profile")
                        .HasColumnType("int");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClinicManager.Domain.Entities.Doctor", b =>
                {
                    b.HasOne("ClinicManager.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("ClinicManager.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("DoctorId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("varchar(64)")
                                .HasColumnName("Country");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("Street");

                            b1.Property<string>("UF")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("varchar(2)")
                                .HasColumnName("UF");

                            b1.HasKey("DoctorId");

                            b1.ToTable("Doctors");

                            b1.WithOwner()
                                .HasForeignKey("DoctorId");
                        });

                    b.OwnsOne("ClinicManager.Domain.ValueObjects.PersonDetail", "PersonDetail", b1 =>
                        {
                            b1.Property<int>("DoctorId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("BirthDate");

                            b1.Property<string>("BloodType")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("varchar(64)")
                                .HasColumnName("BloodType");

                            b1.Property<string>("CPF")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("CPF");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Email");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("LastName");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("DoctorId");

                            b1.ToTable("Doctors");

                            b1.WithOwner()
                                .HasForeignKey("DoctorId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("PersonDetail")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClinicManager.Domain.Entities.Patient", b =>
                {
                    b.HasOne("ClinicManager.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ClinicManager.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("PatientId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("varchar(64)")
                                .HasColumnName("Country");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("varchar(128)")
                                .HasColumnName("Street");

                            b1.Property<string>("UF")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("varchar(2)")
                                .HasColumnName("UF");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.OwnsOne("ClinicManager.Domain.ValueObjects.PersonDetail", "PersonDetail", b1 =>
                        {
                            b1.Property<int>("PatientId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("BirthDate");

                            b1.Property<string>("BloodType")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("varchar(64)")
                                .HasColumnName("BloodType");

                            b1.Property<string>("CPF")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("CPF");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Email");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("LastName");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("PersonDetail")
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
