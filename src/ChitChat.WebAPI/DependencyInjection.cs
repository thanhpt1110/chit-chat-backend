using ChitChat.Infrastructure.ConfigSetting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ChitChat.WebAPI
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddWebAPI(this WebApplicationBuilder builder)
        {
            builder
                .AddJwt()
                .AddSwagger();
            return builder;
        }
        private static WebApplicationBuilder AddJwt(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var services = builder.Services;

            var jwtSettings = configuration.GetSection(nameof(JWTConfigSetting)).Get<JWTConfigSetting>();
            services.AddSingleton<JWTConfigSetting>(jwtSettings);

            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidAudience = jwtSettings.Audience,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateLifetime = true
                    };
                });

            return builder;
        }
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var services = builder.Services;

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ChitChat API",
                    Description = @"All API methods return an object of the generic type <b>ApiResult &lt;T&gt;</b>
                    <br/><br/>{
                      <br/> ""succeeded"": <b>true</b> or <b>false</b>,
                      <br/> ""result"": [the response object if <b>succeeded = true</b>],
                      <br/> ""errors"": [the error(s) if <b>succeeded = false</b>]
                    <br/>}"
                    // TODO: Description, TermsOfService, Contact, License
                });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer YOUR_TOKEN')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id=JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[]{}
                    }
                });
                option.CustomSchemaIds(type => type.ToString());

            });
            return builder;
        }
    }
}
