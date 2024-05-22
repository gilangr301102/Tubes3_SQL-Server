using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;
using System;
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
            // Convert image to ASCII
            string asciiImage = ImageConverter.ConvertImageToAscii(inputSidikJari.BerkasCitra);
            if (asciiImage == null)
            {
                return StatusCode(500, "Error converting image to ASCII");
            }

            // Search for the ASCII image in the SidikJari database using Boyer-Moore algorithm
            SidikJari matchedSidikJari = FindSidikJariByAsciiImage(asciiImage);
            if (matchedSidikJari != null)
            {
                // Search for the name in Biodata using Boyer-Moore algorithm
                BoyerMoore boyerMoore = new BoyerMoore();
                List<int> nameOccurrences = boyerMoore.Search(GetBiodataByName(matchedSidikJari.BiodataName), matchedSidikJari.Nama);

                if (nameOccurrences.Count > 0)
                {
                    // Assuming you want to return the first occurrence
                    return Ok($"Matched Name: {matchedSidikJari.Nama}");
                }
                else
                {
                    // Perform similarity calculation using LCS algorithm
                    var lcs = new LCS();
                    int similarity = lcs.ComputeSimilarity(inputSidikJari.BerkasCitra, matchedSidikJari.BerkasCitra);
                    return Ok($"Similarity: {similarity}");
                }
            }

            return NotFound("No matching SidikJari found for the ASCII image provided.");
        }

        // Helper method to find SidikJari by ASCII image using Boyer-Moore algorithm
        private SidikJari FindSidikJariByAsciiImage(string asciiImage)
        {
            // Initialize BoyerMoore algorithm
            BoyerMoore boyerMoore = new();

            foreach (var sidikJari in _sidikJariDatabase)
            {
                // Search for the asciiImage in the database
                List<int> imageOccurrences = boyerMoore.Search(sidikJari.BerkasCitra, asciiImage);
                if (imageOccurrences.Count > 0)
                {
                    // Assuming you want to return the first occurrence
                    return sidikJari;
                }
            }

            return null; // If no match found
        }

        // Helper method to retrieve Biodata by name
        private Biodata GetBiodataByName(string name)
        {
            // Assuming you have a method to retrieve Biodata from the database based on name
            // Implement this method according to your database access logic
            // For demonstration, I assume there's a method in some BiodataRepository to get Biodata by name
            return BiodataRepository.GetBiodataByName(name);
        }
    }
}
