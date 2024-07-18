namespace Tools.JWT.Helper.Model;

public interface IJwtUser
{
    string Email { get; set; }
    string Id { get; set; }
    IDictionary<string, string> MetaData { get; set; }
    string Roles { get; set; }
}