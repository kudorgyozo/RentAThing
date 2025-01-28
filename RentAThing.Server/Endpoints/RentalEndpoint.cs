using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Handlers.Queries;
using RentAThing.Server.Infrastructure;
using RentAThing.Server.Models;

namespace RentAThing.Server.Endpoints;
public class RentalEndpoint : IEndpointDefinition {

    public string Path => "/api/rentalitems";

    public void DefineEndpoint(WebApplication app) {
        //TODO: how make the user add just one endpoint?
        app.Map("/api/rentalitems", GetRentalItems);
    }

    public async Task<IEnumerable<RentalItem>> GetRentalItems(IMediator mediator, bool? availableOnly) {
        var resp = await mediator.Send(new GetRentalItemsQuery(availableOnly.GetValueOrDefault()));
        return resp;
    }

}

public interface IEndpointDefinition {
    public string Path { get; }

    void DefineEndpoint(WebApplication app);
}
