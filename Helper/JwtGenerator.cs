
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Tools.JWT.Helper.Helper;

public class JwtGenerator(IConfiguration _configuration) : IJwtGenerator
{
    private readonly string issuer = _configuration["JwtIssuer"]!;
    private readonly string baseUrl = _configuration["BaseUrl"]!;
    private readonly DateTime expirationDays = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"])); 
    private readonly SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
    private readonly ClaimBuilder _claimBuilder = new(); 


    /// <summary>
    /// Creates a JWT token type string based on the configuration of the site and the entry params of the method.
    /// </summary>
    /// <param name="userRoles">Roles of the user. Ex Super Admin</param>
    /// <param name="email">Email of the user. Ex. jhon.doe@gmail.com</param>
    /// <param name="id">Id of the user</param>
    /// <param name="metaData">Extra data to be saved on the token</param>
    /// <returns>Created token as string</returns>
    public string GenerateUserToken(string[] userRoles, string email, string id, IDictionary<string, string>? metaData = null)
    {
        _claimBuilder.AddEmail(email)
            .AddGeneratedJti()
            .AddNameIdentifier(id)
            .AddRoles(userRoles);

        if(metaData != null)
            _claimBuilder.AddMetaData(metaData);

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            baseUrl,
            _claimBuilder.Build(),
            expires: expirationDays,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token).ToString();
    }
}
