using MediatR;
using RentAThing.Server.Application.Handlers;
using RentAThing.Server.Application.Handlers.Queries;
using RentAThing.Server.Models;

namespace RentAThing.Server.Controllers.Endpoints;

//this is just some test based on nick chapsas example
public class RentalEndpoint : IEndpointDefinition {

    public void DefineEndpoint(WebApplication app) {
        //TODO: how make the user add just one endpoint?
        app.Map("/api/rentalitems", GetRentalItems);
    }

    public async Task<IEnumerable<ItemDto>> GetRentalItems(IMediator mediator, bool? availableOnly) {
        var resp = await mediator.Send(new GetRentalItemsQuery(availableOnly.GetValueOrDefault()));
        return resp;
    }

}

public interface IEndpointDefinition {


    void DefineEndpoint(WebApplication app);

}
