﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Database.Data;
using api.Utils.Helper;
using static api.Utils.Helper.Enum;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240608213321_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("api.Models.Biodata", b =>
            {
            b.Property<string>("NIK")
                .HasColumnType("nvarchar(450)");

            b.Property<string>("agama")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("alamat")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("golongan_darah")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<KelaminEnum>("jenis_kelamin")
                .HasColumnType("nvarchar(max)");

            b.Property<string>("kewarganegaraan")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("nama")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("pekerjaan")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<status_perkawinanEnum>("status_perkawinan")
                .HasColumnType("nvarchar(max)");

            b.Property<string>("tanggal_lahir")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("tempat_lahir")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.HasKey("NIK");

            b.ToTable("biodata");

            b.HasData(
                new
                {
                    NIK = AesEncryption.EncryptString("1352123456"),
                    agama = AesEncryption.EncryptString("Religion A"),
                    alamat = AesEncryption.EncryptString("Address A"),
                    golongan_darah = AesEncryption.EncryptString("O"),
                    jenis_kelamin = AesEncryption.EncryptString("LakiLaki"),
                    kewarganegaraan = AesEncryption.EncryptString("Country A"),
                    nama = AesEncryption.EncryptString("John Doe"),
                    pekerjaan = AesEncryption.EncryptString("Job A"),
                    status_perkawinan = AesEncryption.EncryptString("BelumMenikah"),
                    tanggal_lahir = AesEncryption.EncryptString("1990-01-01"),
                    tempat_lahir = AesEncryption.EncryptString("City A")
                },
                new
                {
                    NIK = AesEncryption.EncryptString("1352123457"),
                    agama = AesEncryption.EncryptString("Religion B"),
                    alamat = AesEncryption.EncryptString("Address B"),
                    golongan_darah = AesEncryption.EncryptString("A"),
                    jenis_kelamin = AesEncryption.EncryptString("Perempuan"),
                    kewarganegaraan = AesEncryption.EncryptString("Country B"),
                    nama = AesEncryption.EncryptString("Jane Smith"),
                    pekerjaan = AesEncryption.EncryptString("Job B"),
                    status_perkawinan = AesEncryption.EncryptString("Menikah"),
                    tanggal_lahir = AesEncryption.EncryptString("1992-02-02"),
                    tempat_lahir = AesEncryption.EncryptString("City B")
                });
            // b.HasData(
            //     new
            //     {
            //         NIK = "1352123456",
            //         agama = "Religion A",
            //         alamat = "Address A",
            //         golongan_darah = "O",
            //         jenis_kelamin = 0,
            //         kewarganegaraan = "Country A",
            //         nama = "John Doe",
            //         pekerjaan = "Job A",
            //         status_perkawinan = 0,
            //         tanggal_lahir = "1990-01-01",
            //         tempat_lahir = "City A"
            //     },
            //     new
            //     {
            //         NIK = "1352123457",
            //         agama = "Religion B",
            //         alamat = "Address B",
            //         golongan_darah = "A",
            //         jenis_kelamin = 1,
            //         kewarganegaraan = "Country B",
            //         nama = "Jane Smith",
            //         pekerjaan = "Job B",
            //         status_perkawinan = 1,
            //         tanggal_lahir = "1992-02-02",
            //         tempat_lahir = "City B"
            //     });
            });

            modelBuilder.Entity("api.Models.SidikJari", b =>
            {
                b.Property<string>("berkas_citra")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("nama")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("berkas_citra");

                b.ToTable("sidik_jari");

                b.HasData(
                    new
                    {
                        berkas_citra = AesEncryption.EncryptString("FingerprintImage1"),
                        nama = AesEncryption.EncryptString("John Doe")
                    },
                    new
                    {
                        berkas_citra = AesEncryption.EncryptString("FingerprintImage2"),
                        nama = AesEncryption.EncryptString("Jane Smith")
                    });
            });
#pragma warning restore 612, 618
        }
    }
}

