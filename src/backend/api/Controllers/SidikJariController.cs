using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;
using System;
using System.Collections.Generic;
using api.Repositories;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

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

        // POST api/biodata
        [HttpPost("biodata")]
        public IActionResult PostBiodata([FromBody] BiodataRequest inputBiodata)
        {
            var matchedBiodata = _biodataRepository.GetBiodataByName(inputBiodata.nama);

            if (matchedBiodata != null && matchedBiodata.Count > 0)
            {
                return Ok(matchedBiodata);
            }
            else
            {
                return Ok("No matching biodata found.");
            }
        }

        // POST api/sidikjari
        [HttpPost("sidikjari")]
        public IActionResult PostSidikJari([FromBody] SidikJariRequest inputSidikJari)
        {
            var matchedSidikJari = _sidikJariRepository.GetSidikJariByberkas_citra(inputSidikJari.berkas_citra);

            if (matchedSidikJari != null)
            {
                return Ok(matchedSidikJari);
            }
            else
            {
                return Ok("No matching Sidik Jari found.");
            }
        }
    }
}
