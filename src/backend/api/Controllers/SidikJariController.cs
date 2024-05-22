using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;
using System;
using System.Collections.Generic;
using api.Repositories;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SidikJariController : ControllerBase
    {
        private readonly List<SidikJari> _sidikJariDatabase = new(); // Assume this is your database
        private readonly List<Biodata> _biodataDatabase = new();

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
            SidikJari matchedSidikJari = this.FindSidikJariByAsciiImage(asciiImage);
            if (matchedSidikJari != null)
            {
                // Search for the name in Biodata using Boyer-Moore algorithm
                Biodata mathedBiodata = this.FindBiodataByName(matchedSidikJari.Nama);

                if (mathedBiodata != null)
                {
                    return Ok($"Matched Name: {matchedSidikJari.Nama}");
                }

                return Ok($"Similarity: {LCS.ComputeSimilarity(inputSidikJari.BerkasCitra, mathedBiodata.Nama)}");
            }

            return Ok($"Similarity: {LCS.ComputeSimilarity(inputSidikJari.BerkasCitra, matchedSidikJari.BerkasCitra);}");
        }

        // Helper method to find SidikJari by ASCII image using Boyer-Moore algorithm
        private SidikJari? FindSidikJariByAsciiImage(string asciiImage)
        {
            foreach (var sidikJari in _sidikJariDatabase)
            {
                // Search for the asciiImage in the database
                List<int> imageOccurrences = BoyerMoore.Search(sidikJari.BerkasCitra, asciiImage);
                if (imageOccurrences.Count > 0)
                {
                    // Assuming you want to return the first occurrence
                    return sidikJari;
                }
            }

            return null; // If no match found
        }

        private Biodata? FindBiodataByName(string name)
        {
            foreach (var biodata in _biodataDatabase)
            {
                List<int> imageOccurrences = BoyerMoore.Search(biodata.Nama, name);
                if (imageOccurrences.Count > 0)
                {
                    return biodata;
                }
            }

            return null;
        }

        private int ComputeSimilarityBerkasCitra(string berkasCitra)
        {

        }

        private int ComputeSimilarityName(string name)
        {
            int maxSimilarity = 0;
            foreach(var biodata in _biodataDatabase)
            {
                int similarity = LCS.ComputeSimilarity(biodata.Nama, name);
                if (maxSimilarity < similarity)
                {
                    maxSimilarity = similarity;
                }
            }

            return maxSimilarity;
        }
    }
}
