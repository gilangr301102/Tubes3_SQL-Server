namespace api.Models
{
    public class SidikJariResponse
    {
        public required string berkas_citra { get; set; }
        public required string nama { get; set; }
    }

    public class SidikJariRequest
    {
        public int Id { get; set; }
        public string berkas_citra { get; set; }
        public string nama { get; set; }
    }
}
