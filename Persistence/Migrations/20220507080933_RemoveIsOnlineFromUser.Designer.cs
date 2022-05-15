﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(CovidHelperContext))]
    [Migration("20220507080933_RemoveIsOnlineFromUser")]
    partial class RemoveIsOnlineFromUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Institution");

                    b.HasKey("AdminId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Domain.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppointmentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Appointment");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<string>("Pill")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Pill");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("AppointmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Domain.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdminId")
                        .HasColumnType("int")
                        .HasColumnName("AdminId");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkExperience")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.HasIndex("AdminId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Domain.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReceiverDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSenderDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("ReceiverId");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("SenderId");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domain.Pacient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorId");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Pacients");
                });

            modelBuilder.Entity("Domain.Questionaire", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Comments");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date");

                    b.Property<int>("PacientId")
                        .HasColumnType("int")
                        .HasColumnName("PacientId");

                    b.Property<double>("Temperature")
                        .HasColumnType("float")
                        .HasColumnName("Temperature");

                    b.HasKey("QuestionId");

                    b.HasIndex("PacientId");

                    b.ToTable("Questionaire");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdminId")
                        .HasColumnType("int")
                        .HasColumnName("AdminId");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DoctorId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<int?>("PacientId")
                        .HasColumnType("int")
                        .HasColumnName("PacientId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.HasKey("UserId");

                    b.HasIndex("AdminId")
                        .IsUnique()
                        .HasFilter("[AdminId] IS NOT NULL");

                    b.HasIndex("DoctorId")
                        .IsUnique()
                        .HasFilter("[DoctorId] IS NOT NULL");

                    b.HasIndex("PacientId")
                        .IsUnique()
                        .HasFilter("[PacientId] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Appointment", b =>
                {
                    b.HasOne("Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Doctor", b =>
                {
                    b.HasOne("Domain.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Domain.Message", b =>
                {
                    b.HasOne("Domain.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("Domain.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Domain.Pacient", b =>
                {
                    b.HasOne("Domain.Doctor", "Doctor")
                        .WithMany("Pacients")
                        .HasForeignKey("DoctorId");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Domain.Questionaire", b =>
                {
                    b.HasOne("Domain.Pacient", "Pacient")
                        .WithMany("Questionaires")
                        .HasForeignKey("PacientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacient");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.Admin", "Admin")
                        .WithOne("User")
                        .HasForeignKey("Domain.User", "AdminId");

                    b.HasOne("Domain.Doctor", "Doctor")
                        .WithOne("User")
                        .HasForeignKey("Domain.User", "DoctorId");

                    b.HasOne("Domain.Pacient", "Pacient")
                        .WithOne("User")
                        .HasForeignKey("Domain.User", "PacientId");

                    b.HasOne("Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Doctor");

                    b.Navigation("Pacient");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Admin", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Doctor", b =>
                {
                    b.Navigation("Pacients");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Pacient", b =>
                {
                    b.Navigation("Questionaires");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
