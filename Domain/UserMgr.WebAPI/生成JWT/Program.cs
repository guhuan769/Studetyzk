using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

List<Claim> claims = new List<Claim>();
claims.Add(new Claim("Passport","123456"));
claims.Add(new Claim("QQ","888888"));
claims.Add(new Claim("Id","6666"));
claims.Add(new Claim(ClaimTypes.NameIdentifier, "111111"));
claims.Add(new Claim(ClaimTypes.Name, "yzk"));
claims.Add(new Claim(ClaimTypes.HomePhone, "999"));
claims.Add(new Claim(ClaimTypes.Role, "manager"));
claims.Add(new Claim(ClaimTypes.Role, "admin"));
string key = "jfiis*93748392*#jljl:}:";
DateTime expire = DateTime.Now.AddHours(1);
byte[] secBytes = Encoding.UTF8.GetBytes(key);
var secKey = new SymmetricSecurityKey(secBytes);
var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
var tokenDescriptor = new JwtSecurityToken(claims: claims,
    expires: expire, signingCredentials: credentials);
string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
Console.WriteLine(jwt);