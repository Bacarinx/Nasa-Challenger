using back.Context;
using back.Models.Request;
using back.Models.Response;
using back.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Services.LoggedUser;

namespace back.Controller
{
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly NasaChallengeContextDb _context;
        private readonly IHttpContextAccessor _httpContext;
        public RegisterController(NasaChallengeContextDb context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpContext = httpcontext;
        }


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

        [HttpGet]
        [Route("[controller]/GetUser")]
        public IActionResult GetUser()
        {
            var loggedUser = new LoggedUser(_httpContext);
            var userl = loggedUser.GetUser(_context);

            var userDto = new ResponseUserDTO
            {
                Id = userl.Id,
                Name = userl.Name,
                City = userl.City,
                Country = userl.Country,
                Neighborhood = userl.Neighborhood,
                Number = userl.Number,
                State = userl.State,
                Street = userl.Street
            };

            return Ok(userDto);
        }
    }
}
