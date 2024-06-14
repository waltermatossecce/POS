using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            //Documentación de nuestro swagger
            var openApi = new OpenApiInfo
            {
                Title = "POS API",
                Version = "v1",
                Description = "Punto de venta API 2024",
                TermsOfService = new Uri("https://opensource.org/licenses/NII"),
                Contact = new OpenApiContact
                {
                    Name = "SIR TECH S.A.C",
                    Email = "Sirtech@gmail.com",
                    Url = new Uri("https://sirtech.com.pe")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://opensource.org/licenses")
                }
            };

            //generación de swagger
            services.AddSwaggerGen(x =>
            {
                openApi.Version = "V1";
                x.SwaggerDoc("v1", openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme,new string[]{}
                    }  
                });
            });
            return services;
        }
    }
}
