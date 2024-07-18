
namespace Tools.JWT.Helper.Helper;

public interface IJwtGenerator
{
    string GenerateUserToken(string[] userRoles,
        string email,
        string id,
        IDictionary<string, string>? metaData = null);
}