using back.Context;
using back.Models.Entities;
using back.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Services.LoggedUser;

namespace back.Controller
{
    [ApiController]
    public class UserDiseasesController : ControllerBase
    {
        private readonly NasaChallengeContextDb _context;
        private readonly IHttpContextAccessor _httpcontext;

        public UserDiseasesController(NasaChallengeContextDb context, IHttpContextAccessor contextAcessor)
        {
            _context = context;
            _httpcontext = contextAcessor;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetUserDiseases()
        {
            var useCase = new UserDiseasesUseCase(_context, _httpcontext);
            var res = await useCase.GetRespiratoryDiseases();
            return Ok(res);
        }
    }
}