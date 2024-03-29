﻿// <auto-generated />
using System;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace COPDistrictMS.WebApi.Migrations.COPDistrictMS
{
    [DbContext(typeof(COPDistrictMSContext))]
    [Migration("20231006134539_AddedOfferings")]
    partial class AddedOfferings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("MemberId")
                        .HasColumnType("TEXT");

                    b.Property<string>("NearLandmark")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostGps")
                        .HasColumnType("TEXT");

                    b.Property<string>("Residence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telephone")
                        .HasColumnType("TEXT");

                    b.HasKey("MemberId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Assembly", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("YearEstablished")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Assemblies");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.AssemblyOffering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AssemblyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("OfferingType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId");

                    b.ToTable("AssemblyOfferings");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.AssemblyPresiding", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AssemblyId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId")
                        .IsUnique();

                    b.HasIndex("MemberId");

                    b.ToTable("AssemblyPresiding");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("DistrictPastorFullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Manager", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AssemblyId")
                        .HasColumnType("TEXT");

                    b.HasKey("Username");

                    b.HasIndex("AssemblyId");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AssemblyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GivingName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OfficerTypeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherNames")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotographUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssemblyId");

                    b.HasIndex("OfficerTypeId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.MinistryOffering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ministry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MinistryOfferings");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.OfficerType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Office")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OfficerTypes");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Address", b =>
                {
                    b.HasOne("COPDistrictMS.Domain.Entities.Member", "Member")
                        .WithOne("Address")
                        .HasForeignKey("COPDistrictMS.Domain.Entities.Address", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Assembly", b =>
                {
                    b.HasOne("COPDistrictMS.Domain.Entities.District", "District")
                        .WithMany("Assemblies")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.AssemblyOffering", b =>
                {
                    b.HasOne("COPDistrictMS.Domain.Entities.Assembly", "Assembly")
                        .WithMany("Offerings")
                        .HasForeignKey("AssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assembly");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.AssemblyPresiding", b =>
                {
                    b.HasOne("COPDistrictMS.Domain.Entities.Assembly", "Assembly")
                        .WithOne("PresidingOfficer")
                        .HasForeignKey("COPDistrictMS.Domain.Entities.AssemblyPresiding", "AssemblyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("COPDistrictMS.Domain.Entities.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assembly");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Manager", b =>
                {
                    b.HasOne("COPDistrictMS.Domain.Entities.Assembly", null)
                        .WithMany("Managers")
                        .HasForeignKey("AssemblyId");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Member", b =>
                {
                    b.HasOne("COPDistrictMS.Domain.Entities.Assembly", "Assembly")
                        .WithMany()
                        .HasForeignKey("AssemblyId");

                    b.HasOne("COPDistrictMS.Domain.Entities.OfficerType", "OfficerType")
                        .WithMany("Members")
                        .HasForeignKey("OfficerTypeId");

                    b.Navigation("Assembly");

                    b.Navigation("OfficerType");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Assembly", b =>
                {
                    b.Navigation("Managers");

                    b.Navigation("Offerings");

                    b.Navigation("PresidingOfficer");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.District", b =>
                {
                    b.Navigation("Assemblies");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.Member", b =>
                {
                    b.Navigation("Address");
                });

            modelBuilder.Entity("COPDistrictMS.Domain.Entities.OfficerType", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
