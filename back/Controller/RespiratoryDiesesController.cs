using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using back.Context;
using back.Models.Entities;
using back.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;

namespace back.Controller
{
    [ApiController]
    public class RespiratoryDiesesController : ControllerBase
    {
        public RespiratoryDiesesController(NasaChallengeContextDb context)
        {
            _context = context;
        }
        private readonly NasaChallengeContextDb _context;

        [HttpGet]
        [Route("[controller]")]
        [ProducesResponseType(typeof(List<RespiratoryDiseases>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var useCase = new RespiratoryDiseasesUseCase();
            var response = await useCase.GetAsync(_context);
            return Ok(response);
        }
    }
}