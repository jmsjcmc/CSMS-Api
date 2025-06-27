using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Helpers.Queries;
using CSMapi.Services;
using CSMapi.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CSMapi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScopeService(this IServiceCollection service)
        {
            // Excel Utils
            service.AddScoped<PalletExcel>();
            service.AddScoped<CustomerExcel>();
            service.AddScoped<ProductExcel>();
            service.AddScoped<ReceivingExcel>();
            service.AddScoped<DispatchingExcel>();
            // Helpers
            service.AddScoped<ExcelHelper>();
            service.AddScoped<AuthUserHelper>();
            service.AddScoped<DocumentHelper>();
            // Validators
            service.AddScoped<CustomerValidator>();
            service.AddScoped<ContractValidator>();
            service.AddScoped<DispatchingValidator>();
            service.AddScoped<ReceivingValidator>();
            service.AddScoped<UserValidator>();
            service.AddScoped<PalletValidator>();
            service.AddScoped<ProductValidator>();
            // Services
            service.AddScoped<ReceivingService>();
            service.AddScoped<DispatchingService>();
            service.AddScoped<UserService>();
            service.AddScoped<ContractService>();
            service.AddScoped<CustomerService>();
            service.AddScoped<PalletService>();
            service.AddScoped<ProductService>();
            // Queries
            service.AddScoped<ContractQueries>();
            service.AddScoped<CustomerQueries>();
            service.AddScoped<DispatchingQueries>();
            service.AddScoped<PalletQueries>();
            service.AddScoped<ProductQueries>();
            service.AddScoped<ReceivingQueries>();
            service.AddScoped<UserQueries>();
            return service;
        }
        // Swagger Documentation
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "CSM-api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Bearer (jjwt_token)",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return service;
        }
        // JWT Authentication
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Headers["Authorization"].ToString();
                        if (!string.IsNullOrEmpty(accessToken) && !accessToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return service;
        }
        // CORS
        public static IServiceCollection AddCustomerCORS(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            return service;
        }
        // Path for saving images 
        public static void ConfigureStaticFiles(this WebApplication app)
        {
            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(@"C:\Users\James\Desktop\CSMS\Avatar"),
            //    RequestPath = "/uploads/avatar"
            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(@"D:\CISDEVO Repo\e-sign"),
            //    RequestPath = "/uploads/esignature"
            //});
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(@"D:\CISDEVO Repo\images"),
            //    RequestPath = "/uploads/receivingform"
            //});
        }
    }
}
