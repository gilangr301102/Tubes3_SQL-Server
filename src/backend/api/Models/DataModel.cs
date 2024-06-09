using System.Collections.Generic;

namespace api.Models
{
    public class DataModel
    {
        public List<string> names { get; set; }
        public List<string> jobs { get; set; }
        public List<string> nationalities { get; set; }
        public List<string> religions { get; set; }
        public List<string> addresses { get; set; }
        public List<string> cityNames { get; set; }
        public List<string> bloodTypes { get; set; }
        public List<string> maritalStatuses { get; set; }
    }

    public class APIResponse
    {
        public ICollection<BiodataResponse>? biodataRes { get; set; }
        public SidikJariResponse? sidikJariRes { get; set; }
        public string message { get; set; }
        public string timeExecution {  get; set; }
    }
}
