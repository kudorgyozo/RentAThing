namespace RentAThing.Server.Controllers.Endpoints {
    public static class EndpointExtensions {
        public static void RegisterEndpointDefinitions(this WebApplication app) {
            foreach (var endpoint in GetEndpointDefinitions()) {
                endpoint.DefineEndpoint(app);
            }
        }

        static IEnumerable<IEndpointDefinition> GetEndpointDefinitions() {
            // Discover and register all endpoint definitions dynamically
            return typeof(IEndpointDefinition).Assembly
                .GetTypes()
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();
        }
    }
}
