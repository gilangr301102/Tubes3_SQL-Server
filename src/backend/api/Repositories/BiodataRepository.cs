using System;
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

        public ICollection<BiodataResponse> GetBiodataByName(string name, int algorithm = 0)
        {
            var biodatas = _context.biodata.ToList();
            var result = new List<BiodataResponse>();

            foreach (var biodata in biodatas)
            {
                // Decrypt each property individually
                biodata.NIK = AesEncryption.DecryptString(biodata.NIK);
                biodata.nama = AesEncryption.DecryptString(biodata.nama);
                biodata.alamat = AesEncryption.DecryptString(biodata.alamat);
                biodata.pekerjaan = AesEncryption.DecryptString(biodata.pekerjaan);
                biodata.tempat_lahir = AesEncryption.DecryptString(biodata.tempat_lahir);
                biodata.tanggal_lahir = AesEncryption.DecryptString(biodata.tanggal_lahir);
                biodata.agama = AesEncryption.DecryptString(biodata.agama);
                biodata.golongan_darah = AesEncryption.DecryptString(biodata.golongan_darah);
                biodata.status_perkawinan = AesEncryption.DecryptString(biodata.status_perkawinan);
                biodata.kewarganegaraan = AesEncryption.DecryptString(biodata.kewarganegaraan);
                biodata.jenis_kelamin = AesEncryption.DecryptString(biodata.jenis_kelamin);

                string normalName = ConverterAlayToNormal.KonversiAlayKeNormalLogic(name, biodata.nama);
                Console.WriteLine("debug normal name: ");
                Console.WriteLine(normalName);
                bool isMatch = false;

                if (algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(biodata.nama, normalName);
                }
                else if (algorithm == 1)
                {
                    isMatch = KMP.Search(biodata.nama, normalName);
                }

                if (isMatch)
                {
                    result.Add(biodata);
                }
                else
                {
                    var similarityHandler = new SimilarityAlayHandler(normalName, biodata.nama);
                    if (similarityHandler.GetPercentageOfSimilarityBahasaAlay() >= 0.71)
                    {
                        result.Add(biodata);
                    }
                }
            }

            return result;
        }
    }
}
