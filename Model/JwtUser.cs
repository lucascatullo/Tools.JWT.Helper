using Core.Models.Manager.Exception;
using System.Security.Claims;
using Tools.JWT.Helper.Constant;
using Tools.JWT.Helper.Exceptions;

namespace Tools.JWT.Helper.Model;

public class JwtUser : IJwtUser
{
    /// <summary>
    /// Map JWT claims into the correct Property.
    /// </summary>
    /// <param name="User">Access from your controller this.User</param>
    JwtUser(ClaimsPrincipal User)
    {
        if (User.Claims.Count() < 3)
            throw new Exception<RetrievingUserClaimsException>(new RetrievingUserClaimsException());

        Email = User.Claims.ElementAt(JwtConstants.EMAIL_CLAIM_POSITION).Value;
        Id = User.Claims.ElementAt(JwtConstants.EMAIL_CLAIM_POSITION).Value;
        MapUserRolesAsString(User);
        MapClaimsMetaData(User);
    }

    public string Email { get; set; }
    public string Id { get; set; }
    public IDictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();
    public string Roles { get; set; }


    private void MapUserRolesAsString(ClaimsPrincipal User)
    {
        Roles = "";
        var roleClaims = User.Claims.Where(x => x.Type.Contains("role"));
        foreach (var roleClaim in roleClaims)
            Roles += roleClaim.Value + ',';

    }

    private void MapClaimsMetaData(ClaimsPrincipal User)
    {
        MetaData = new Dictionary<string, string>();
        var claims = User.Claims.Where(x => x.ValueType == "metadata");
        foreach (var claim in claims)
            MetaData.Add(claim.Type.ToString(), claim.Value.ToString());

    }

}