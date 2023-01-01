
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


string jwt = Console.ReadLine();

string secKey = "jfiis*93748392*#jljl:}:";
JwtSecurityTokenHandler tokenHandler = new();
TokenValidationParameters valParam = new();
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKey));
valParam.IssuerSigningKey = securityKey;
valParam.ValidateIssuer = false;
valParam.ValidateAudience = false;
ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt,
            valParam, out SecurityToken secToken);
foreach (var claim in claimsPrincipal.Claims)
{
    Console.WriteLine($"{claim.Type}={claim.Value}");
}

