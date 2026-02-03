using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InterUniversity.Application.Abstractions.Security;

public interface IJWTFactory
{
    string GenerateEncodedToken(ClaimsIdentity identity);
    JwtSecurityToken DecodeToken(string token);
}