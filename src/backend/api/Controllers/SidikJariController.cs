using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Utils.Algorithm;
using System.Collections.Generic;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SidikJariController : ControllerBase
    {
        private readonly List<SidikJari> _sidikJariDatabase = new(); // Assume this is your database

        // POST api/sidikjari
        [HttpPost]
        public IActionResult PostSidikJari([FromBody] SidikJari inputSidikJari)
        {
            // Search for the input sidik jari in the database
            foreach (var sidikJari in _sidikJariDatabase)
            {
                if (sidikJari.BerkasCitra == inputSidikJari.BerkasCitra && sidikJari.Nama == inputSidikJari.Nama)
                {
                    return Ok(sidikJari);
                }
            }

            // If not found, calculate similarity using LCS algorithm
            var lcs = new LCS();
            int similarity = lcs.ComputeSimilarity(inputSidikJari.BerkasCitra, _sidikJariDatabase[0].BerkasCitra); // Assuming you compare with the first sidik jari in the database
            return Ok($"Similarity: {similarity}");
        }

        // POST api/biodata
        [HttpPost]
        public IActionResult PostBiodata([FromBody] Biodata biodata)
        {
            // Handle the biodata
            // You can perform database operations or any other logic here
            return Ok(biodata);
        }
    }
}
