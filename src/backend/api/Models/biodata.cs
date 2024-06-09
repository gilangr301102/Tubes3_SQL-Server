using static api.Utils.Helper.Enum;

namespace api.Models
{
    public class BiodataResponse
    {
        public required string NIK { get; set; }
        public required string nama { get; set; }
        public required string tempat_lahir { get; set; }
        public required string tanggal_lahir { get; set; }
        public required KelaminEnum jenis_kelamin { get; set; }
        public required string golongan_darah { get; set; }
        public required string alamat { get; set; }
        public required string agama { get; set; }
        public required status_perkawinanEnum status_perkawinan { get; set; }
        public required string pekerjaan { get; set; }
        public required string kewarganegaraan { get; set; }
    }


    public class BiodataRequest
    {
        public required string NIK { get; set; }
        public required string nama { get; set; }
        public required string tempat_lahir { get; set; }
        public required string tanggal_lahir { get; set; }
        public required string jenis_kelamin { get; set; }
        public required string golongan_darah { get; set; }
        public required string alamat { get; set; }
        public required string agama { get; set; }
        public required string status_perkawinan { get; set; }
        public required string pekerjaan { get; set; }
        public required string kewarganegaraan { get; set; }
    }
}
