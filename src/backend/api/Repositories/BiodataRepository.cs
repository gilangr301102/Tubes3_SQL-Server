﻿using System;
using System.Collections.Generic;
using System.Linq;
using api.Database.Data;
using api.Interfaces;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;
using api.Utils.Helper;

namespace api.Repositories
{
    public class BiodataRepository : IBiodataRepository
    {
        private readonly DataContext _context;

        public BiodataRepository(DataContext context)
        {
            _context = context;
        }

        public string RemoveNullCharacters(string input)
        {
            return input==null ? "" : input.Replace("\u0000", "");
        }

        public ICollection<BiodataResponse> GetBiodataByName(string name, int algorithm = 0)
        {
            var biodatas = _context.biodata.ToList();
            var result = new List<BiodataResponse>();

            foreach (var biodata in biodatas)
            {
                // Decrypt each property individually
                biodata.NIK = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.NIK));
                biodata.nama = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.nama));
                biodata.alamat = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.alamat));
                biodata.pekerjaan = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.pekerjaan));
                biodata.tempat_lahir = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.tempat_lahir));
                biodata.tanggal_lahir = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.tanggal_lahir));
                biodata.agama = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.agama));
                biodata.golongan_darah = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.golongan_darah));
                biodata.status_perkawinan = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.status_perkawinan));
                biodata.kewarganegaraan = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.kewarganegaraan));
                biodata.jenis_kelamin = RemoveNullCharacters(AesEncryption.DecryptWithPadding(biodata.jenis_kelamin));

                string normalName = ConverterAlayToNormal.GetKonversiArrayToNormal(name, biodata.nama);
                string lowerNormalName = biodata.nama.ToLower();
                bool isMatch = false;

                double similarityPercentage = 0.0; // Initialize similarity percentage

                if (algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(lowerNormalName, normalName);
                }
                else if (algorithm == 1)
                {
                    isMatch = KMP.Search(lowerNormalName, normalName);
                }

                if (isMatch)
                {
                    similarityPercentage = 1.0; // Set similarity to 100% if exact match
                }
                else
                {
                    var similarityHandler = new SimilarityAlayHandler(normalName, lowerNormalName);
                    similarityPercentage = similarityHandler.GetPercentageOfSimilarityBahasaAlay();
                }

                if(similarityPercentage >= 0.70)
                {
                    similarityPercentage *= 100;
                    result.Add(new BiodataResponse
                    {
                        NIK = biodata.NIK,
                        nama = biodata.nama,
                        tempat_lahir = biodata.tempat_lahir,
                        tanggal_lahir = biodata.tanggal_lahir,
                        jenis_kelamin = biodata.jenis_kelamin,
                        golongan_darah = biodata.golongan_darah,
                        alamat = biodata.alamat,
                        agama = biodata.agama,
                        status_perkawinan = biodata.status_perkawinan,
                        pekerjaan = biodata.pekerjaan,
                        kewarganegaraan = biodata.kewarganegaraan,
                        similarity = similarityPercentage.ToString("F2")+"%" // Set similarity percentage
                    });
                }
            }

            return result;
        }
    }
}
