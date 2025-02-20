using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using RentAThing.Server.Application.Interfaces;
using RentAThing.Server.Application.Services;
using RentAThing.Server.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

IdentityModelEventSource.ShowPII = true;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => {
    options.Filters.Add<GlobalExceptionFilter>(); // Add the global exception filter
});
builder.Services.AddOpenApi();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        var key = builder.Configuration["Jwt:Key"]!;

        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true, // Validate the server that issued the token
            ValidateAudience = true, // Validate the recipient of the token
            ValidateLifetime = true, // Validate the token's expiration
            ValidateIssuerSigningKey = true, // Validate the signature
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero

        };
        options.MapInboundClaims = false;
        options.Events = new JwtBearerEvents {

            OnMessageReceived = context => {
                Console.WriteLine($"Token received: {context.Request.Headers.Authorization}");
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context => {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context => {
                var claims = context.Principal!.Claims;
                foreach (var claim in claims) {
                    Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                }
                Console.WriteLine("Token validated successfully.");
                return Task.CompletedTask;

            }
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminPolicy", policy => {
        policy.RequireClaim("admin", "1");
    });


builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqLite"))
        .UseSeeding((context, _) => {
            SeedDataUtils.AddSeedData((AppDbContext)context);
        });
});

//builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqLite")));
builder.Services.AddMediatR(x => {
    x.RegisterServicesFromAssemblyContaining<Program>();
    //x.Lifetime = ServiceLifetime.Scoped;
});

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IRentRepo, RentRepo>();

var app = builder.Build();

SeedDataUtils.DropCreateDB(app);

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.RegisterEndpointDefinitions(); //just a silly test with some custom endpoints

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

