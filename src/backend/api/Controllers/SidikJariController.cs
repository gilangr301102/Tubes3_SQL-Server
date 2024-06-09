using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;
using System;
using System.Collections.Generic;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SidikJariController : ControllerBase
    {
        private readonly List<SidikJariResponse> _sidikJariDatabase = new(); // Assume this is your database
        private readonly List<BiodataResponse> _biodataDatabase = new();

        private readonly ILogger<SidikJariController> _logger;
        private readonly DbContext _context;

        public SidikJariController(ILogger<SidikJariController> logger, DbContext sidikJariDbContext)
        {
            _logger = logger;
            _context = sidikJariDbContext;
        }

        // POST api/sidikjari
        [HttpPost]
        public IActionResult PostSidikJari([FromBody] SidikJariResponse inputSidikJari)
        {
            // Convert image to ASCII
            //string asciiImage = ImageConverter.ConvertImageToAscii(inputSidikJari.berkas_citra);
            //if (asciiImage == null)
            //{
            //    return StatusCode(500, "Error converting image to ASCII");
            //}

            //// Search for the ASCII image in the SidikJariResponse database using Boyer-Moore algorithm
            //SidikJariResponse matchedSidikJari = this.FindSidikJariByAsciiImage(asciiImage);

            //if (matchedSidikJari == null) return Ok($"Similarity: {ComputeSimilarityberkas_citra(asciiImage)}");
            return Ok($"Matched Name: ");
            //return Ok($"Matched Name: {matchedSidikJari.nama}");
        }

        //// Helper method to find SidikJariResponse by ASCII image using Boyer-Moore algorithm
        //private SidikJariResponse? FindSidikJariByAsciiImage(string asciiImage)
        //{
        //    foreach (var sidikJari in _sidikJariDatabase)
        //    {
        //        // Search for the asciiImage in the database
        //        bool isCitraFound = BoyerMoore.Search(sidikJari.berkas_citra, asciiImage);
        //        if (isCitraFound)
        //        {
        //            // Assuming you want to return the first occurrence
        //            return sidikJari;
        //        }
        //    }

        //    return null; // If no match found
        //}

        //private int ComputeSimilarityberkas_citra(string berkasCitra)
        //{
        //    int maxSimilarity = 0;
        //    foreach(var sidikJari in _sidikJariDatabase)
        //    {
        //        int similarity = LCS.ComputeSimilarity(sidikJari.berkas_citra, berkasCitra);
        //        if(maxSimilarity < similarity)
        //        {
        //            maxSimilarity = similarity;
        //        }
        //    }

        //    return maxSimilarity;
        //}

        //private List<BiodataResponse> getAllBiodataByName(string name)
        //{
        //    var ret = new List<BiodataResponse>();

        //    // To do: Implement bahasa alay searching with pattern matching

        //    return ret;
        //}
    }
}
