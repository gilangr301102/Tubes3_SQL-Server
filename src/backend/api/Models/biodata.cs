using static api.Utils.Helper.Enum;

namespace api.Models
{
    public class Biodata
    {
        public string Nama { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public KelaminEnum JenisKelamin { get; set; }
        public string GolonganDarah { get; set; }
        public string Alamat { get; set; }
        public string Agama { get; set; }
        public StatusPerkawinanEnum StatusPerkawinan { get; set; }
        public string Pekerjaan { get; set; }
        public string Kewarganegaraan { get; set; }
    }
}
