namespace api.Models
{
    public class SidikJariResponse
    {
        public int Id { get; set; }
        public required string berkas_citra { get; set; }
        public required string nama { get; set; }
    }

    public class SidikJariRequest
    {
        public required string berkas_citra { get; set; }
        public required int algorithm { get; set; }
    }

    public class SidikJariMigration
    {
        public int Id { get; set; }
        public string berkas_citra { get; set; }
        public string nama { get; set; }
    }
}
