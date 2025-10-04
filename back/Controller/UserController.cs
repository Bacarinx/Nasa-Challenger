using back.Context;
using back.Models.Request;
using back.Models.Response;
using back.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back.Controller
{
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public RegisterController(NasaChallengeContextDb context)
        {
            _context = context;
        }
        private readonly NasaChallengeContextDb _context;

        [ProducesResponseType(typeof(ResponseUserRegisterJson), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("[controller]/register")]
        public IActionResult Register(RequestUserRegisterJson request)
        {
            var useCase = new UserUseCase();
            var response = useCase.Execute(request, _context);
            return Created(String.Empty, response);
        }

        [ProducesResponseType(typeof(ResponseUserRegisterJson), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("[controller]/login")]
        public IActionResult Login(RequestLogin request)
        {
            var useCase = new UserUseCase();
            var response = useCase.Login(request, _context);
            return Created(String.Empty, response);
        }
    }
}
