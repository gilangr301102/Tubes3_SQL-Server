using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using System.Diagnostics;
using api.Utils.Converter;
using api.Utils.Helper;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SidikJariController : ControllerBase
    {
        private readonly ILogger<SidikJariController> _logger;
        private readonly IBiodataRepository _biodataRepository;
        private readonly ISidikJariRepository _sidikJariRepository;

        public SidikJariController(ILogger<SidikJariController> logger, IBiodataRepository biodataRepository, ISidikJariRepository sidikJariRepository)
        {
            _logger = logger;
            _biodataRepository = biodataRepository;
            _sidikJariRepository = sidikJariRepository;
        }

        // POST api/sidikjari
        [HttpPost("")]
        public IActionResult PostSidikJari([FromBody] SidikJariRequest inputSidikJari)
        {
            // Create a new Stopwatch instance
            Stopwatch stopwatch = new Stopwatch();

            // Start the stopwatch
            stopwatch.Start();

            SidikJariResponse? matchedSidikJari = _sidikJariRepository.GetSidikJariByberkas_citra(inputSidikJari.berkas_citra);

            string message = "No matching data found.";

            ICollection<BiodataResponse>? matchedBiodata = null;

            if (matchedSidikJari != null)
            {
                matchedBiodata = _biodataRepository.GetBiodataByName(matchedSidikJari.nama);

                if (matchedBiodata != null && matchedBiodata.Count > 0)
                {
                    Console.WriteLine($"Reconstructed image saved to {"reconstructed_image.png"}");
                    FileHelper.ConvertBmpToPng(matchedSidikJari.Id-1);
                    message = "Matching data found.";
                }
            }

            // Stop the stopwatch
            stopwatch.Stop();

            // Get the elapsed time as a TimeSpan value
            TimeSpan elapsed = stopwatch.Elapsed;

            var response = new APIResponse
            {
                biodataRes = matchedBiodata,
                sidikJariRes = matchedSidikJari,
                message = message,
                timeExecution = $"{elapsed.TotalMilliseconds} ms",
            };

            return Ok(response);
        }
    }
}
