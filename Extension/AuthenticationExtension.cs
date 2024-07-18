using Core.Models.Manager.Exception;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Tools.JWT.Helper.Extension;

public static class AuthenticationExtension
{
    /// <summary>
    /// Adds the authentication with type JWT to the current app.
    /// </summary>
    /// <param name="builder">response on the AddAuthentication call on the program.cs. This should look like this on your app: builder.AddAuthentication().AuthenticateWithJwt(config)</param>
    /// <param name="config">The IConfiguration should have this parameters:JwtIssuer , BaseUrl , JwtKey </param>
    public static void AuthenticateUsingJwt(this AuthenticationBuilder builder, IConfiguration config)
    {
        string? issuer = config.GetSection("JwtIssuer")?.Value;
        string? baseUrl = config.GetSection("BaseUrl")?.Value;
        string? jwtKey = config.GetSection("JwtKey")?.Value;

        NullException.TrhowIfNull(issuer);
        NullException.TrhowIfNull(baseUrl);
        NullException.TrhowIfNull(jwtKey);

        builder.AddJwtBearer(cfg =>
         {
             cfg.RequireHttpsMetadata = false;
             cfg.SaveToken = true;
             cfg.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidIssuer = issuer,
                 ValidAudience = baseUrl,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                 ClockSkew = TimeSpan.Zero
             };
         });
    }
}