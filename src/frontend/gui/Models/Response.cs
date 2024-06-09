using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gui.Models
{
    // Class to represent Biodata data
    public class BiodataData
    {
        public string nik { get; set; }
        public string nama { get; set; }
        public string tempat_lahir { get; set; }
        public string tanggal_lahir { get; set; }
        public string jenis_kelamin { get; set; }
        public string golongan_darah { get; set; }
        public string alamat { get; set; }
        public string agama { get; set; }
        public string status_perkawinan { get; set; }
        public string pekerjaan { get; set; }
        public string kewarganegaraan { get; set; }
        public string similarity { get; set; }
    }

    // Class to represent SidikJari data
    public class SidikJariData
    {
        public string berkas_citra { get; set; }
        public string nama { get; set; }
        public string similarity { get; set; }
    }

    // Class to represent the entire response data
    public class ApiResponse
    {
        public List<BiodataData> biodataRes { get; set; }
        public SidikJariData sidikJariRes { get; set; }
        public string message { get; set; }
        public string timeExecution { get; set; }
    }
}
