using ChitChat.Infrastructure.ConfigSetting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChitChat.Infrastructure.Authorization
{
    internal static class AuthorizationRegisteration
    {
        public static WebApplicationBuilder AddAppAuthorization(this WebApplicationBuilder builder)
        {
            var jwtOption = builder.Configuration.GetSection(nameof(JWTConfigSetting)).Get<JWTConfigSetting>();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    ValidateAudience = true
                };
            });
            return builder;
        }
    }
}
