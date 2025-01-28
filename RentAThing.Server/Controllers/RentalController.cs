using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentAThing.Server.Handlers.Queries;
using RentAThing.Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentAThing.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController : ControllerBase {
    private readonly IMediator mediator;

    //start rent
    //stop rent
    //get all

    public RentalController(IMediator mediator) {
        this.mediator = mediator;
    }

    // GET: api/<ValuesController>
    [HttpGet]
    public async Task<IEnumerable<RentalItem>> Get(bool? availableOnly) {
        var res = await mediator.Send(new GetRentalItemsQuery(availableOnly ?? false));
        return res;
    }

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
    public string Get(int id) {
        return "value";
    }

    // POST api/<ValuesController>
    [HttpPost]
    public void Post([FromBody] string value) {
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) {
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id) {
    }
}
