using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back.Context;
using back.Hubs;
using back.Models.Entities;
using back.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OrderSolution.API.Services.LoggedUser;

namespace back.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class DependentsController : ControllerBase
    {
        private readonly NasaChallengeContextDb _context;
        private readonly IHubContext<DependentsHub> _hub;
        private readonly IHttpContextAccessor _httpContext;

        public DependentsController(NasaChallengeContextDb context, IHubContext<DependentsHub> hub, IHttpContextAccessor httpContext)
        {
            _context = context;
            _hub = hub;
            _httpContext = httpContext;
        }

        [HttpPost("add-dependent")]
        public async Task<IActionResult> AddDependent([FromBody] string cpf)
        {
            var target = _context.User.FirstOrDefault(u => u.CPF == cpf);

            var loggedUser = new LoggedUser(_httpContext);
            var userl = loggedUser.GetUser(_context);

            if (target == null)
                return NotFound("User Not Found");

            var request = new DependentRequest
            {
                RequesterId = userl.Id,
                TargetId = target.Id
            };

            _context.DependentRequests.Add(request);
            await _context.SaveChangesAsync();

            // Notifica em tempo real o usuário alvo
            await _hub.Clients.User(target.Id.ToString())
                .SendAsync("ReceiveDependentRequest", $"User {userl.Name} want adds you as dependent.");

            return Ok("Request has been Sended.");
        }

        [HttpPost("accept-request/{requestId}")]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            var request = _context.DependentRequests.FirstOrDefault(r => r.Id == requestId);
            if (request == null) return NotFound("Request Not Found");

            var loggedUser = new LoggedUser(_httpContext);
            var userl = loggedUser.GetUser(_context);

            if (userl.Id == request.TargetId)
            {
                request.Accepted = true;
                await _context.SaveChangesAsync();

                await _hub.Clients.User(request.RequesterId.ToString())
                .SendAsync("DependentRequestAccepted", "Request Accepted");

                return Ok("Ok");
            }
            else
            {
                return Ok("Unathorized.");
            }

        }

        [HttpGet]
        public IActionResult GetRequests()
        {
            var loggedUser = new LoggedUser(_httpContext);
            var userl = loggedUser.GetUser(_context);

            var res = _context.DependentRequests.Where(d => d.TargetId == userl.Id).ToList();

            var responses = new List<ResponseDependents>();

            foreach (var d in res)
            {
                var userAux = _context.User.FirstOrDefault(u => u.Id == d.RequesterId);

                responses.Add(new ResponseDependents
                {
                    Id = d.Id,
                    Requester = new UserDTODependents
                    {
                        Id = userAux!.Id,
                        CPF = userAux.CPF,
                        Name = userAux.Name,
                        PhoneNumber = userAux.PhoneNumber
                    },
                    TargetId = d.TargetId,
                    Accepted = d.Accepted,
                    RequestedAt = d.RequestedAt
                });
            }

            return Ok(responses);
        }
    }
}