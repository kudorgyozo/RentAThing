using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAThing.Server.Application.Handlers;
using RentAThing.Server.Application.Handlers.Queries;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAThing.Server.Controllers {
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController(IMediator mediator) : ControllerBase {
        private readonly IMediator mediator = mediator;

        // GET: api/<ProfileController>
        [HttpGet("items/active")]
        public async Task<IEnumerable<ItemDto>> GetActive() {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value;
            var res = await mediator.Send(new GetActiveItemsQuery(int.Parse(userId)));
            return res;
        }

    }
}
