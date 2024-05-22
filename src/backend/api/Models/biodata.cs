using static api.Utils.Helper.Enum;

namespace api.Models
{
    public class Biodata
    {
        public required string Nama { get; set; }
        public required string TempatLahir { get; set; }
        public required string TanggalLahir { get; set; }
        public required KelaminEnum JenisKelamin { get; set; }
        public required string GolonganDarah { get; set; }
        public required string Alamat { get; set; }
        public required string Agama { get; set; }
        public required StatusPerkawinanEnum StatusPerkawinan { get; set; }
        public required string Pekerjaan { get; set; }
        public required string Kewarganegaraan { get; set; }
    }
}
