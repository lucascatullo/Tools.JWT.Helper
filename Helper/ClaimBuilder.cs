

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Tools.JWT.Helper.Helper;

internal class ClaimBuilder
{
    private IList<Claim> _claims = [];


    public ClaimBuilder AddEmail(string email)
    {
        _claims.Add(new(JwtRegisteredClaimNames.Sub, email));
        return this;
    }


    public ClaimBuilder AddGeneratedJti()
    {
        _claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        return this;
    }

    public ClaimBuilder AddNameIdentifier(string id)
    {
        _claims.Add(new(ClaimTypes.NameIdentifier, id));
        return this;
    } 


    public ClaimBuilder AddRoles(string[] roles)
    {
        foreach (var role in roles)
            _claims.Add(new Claim(ClaimTypes.Role, role));
        return this; 
    }

    public ClaimBuilder AddMetaData(IDictionary<string, string> metadata)
    {
        foreach (var data in metadata) _claims.Add(new Claim(data.Key, data.Value, "metadata"));

        return this;
    }


    public IList<Claim> Build() => _claims;


}
