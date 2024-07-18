

using System.Security.Claims;

namespace Tools.JWT.Helper.Helper;

internal class ClaimBuilder
{
    private IList<Claim> _claims = [];


    public ClaimBuilder AddEmail(string email)
    {
        return this;
    }


    public ClaimBuilder AddGeneratedJti()
    {
        return this;
    }

    public ClaimBuilder AddNameIdentifier(string id)
    {
        return this;
    } 


    public ClaimBuilder AddRoles(string[] roles)
    {
        return this; 
    }

    public ClaimBuilder AddMetaData(IDictionary<string, string> metadata)
    {
        return this;
    }


    public IList<Claim> Build() => _claims;


}
