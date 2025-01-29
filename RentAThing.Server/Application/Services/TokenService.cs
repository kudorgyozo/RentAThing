using Microsoft.IdentityModel.Tokens;
using RentAThing.Server.Application.Handlers.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentAThing.Server.Application.Services;
public class TokenService(IConfiguration configuration) {
    public (string, Dictionary<string, string>) GenerateToken(string username, int id) {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
            new(JwtRegisteredClaimNames.Name, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (username == "admin") {
            claims.Add(new Claim("admin", "1"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(3000),
            signingCredentials: credentials
        );

        Dictionary<string, string> clm = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToDictionary(c => c.Type!, c => c.Value!);

        var tkn = new JwtSecurityTokenHandler().WriteToken(token);
        return (tkn, clm);
    }
}
