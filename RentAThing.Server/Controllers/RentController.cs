using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAThing.Server.Application.Handlers;
using RentAThing.Server.Application.Handlers.Commands;
using RentAThing.Server.Application.Handlers.Queries;
using RentAThing.Server.Models;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAThing.Server.Controllers;

[ApiController]
[Route("api/rent")]
public class RentController(IMediator mediator) : ControllerBase {

    // GET: api/<ValuesController>
    [HttpGet()]
    public async Task<IEnumerable<ItemDto>> Get(bool? availableOnly) {
        var res = await mediator.Send(new GetRentalItemsQuery(availableOnly ?? false));
        return res;
    }

    [HttpPut("{id}/start")]
    [Authorize]
    public async Task RentItem([FromRoute] int id) {
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value;
        await mediator.Send(new StartRentCommand(int.Parse(userId), id));

    }

    [HttpPut("{id}/stop")]
    [Authorize]
    public async Task ReturnItem([FromRoute] int id) {
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value;
        await mediator.Send(new StopRentCommand(int.Parse(userId), id));
    }

    //// GET api/<ValuesController>/5
    //[HttpGet("{id}")]
    //public string Get(int id) {
    //    return "value";
    //}

    //// POST api/<ValuesController>
    //[HttpPost]
    //public void Post([FromBody] string value) {
    //}

    //// PUT api/<ValuesController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value) {
    //}

    //// DELETE api/<ValuesController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id) {
    //}
}
