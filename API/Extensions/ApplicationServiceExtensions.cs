
using Application.Activities;
using Application.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddOpenApi();
            services.AddDbContext<DataContext>(opt =>
                {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); // Connection to the database
                });

            services.AddCors(opt =>{
                opt.AddPolicy("CorsPolicy", policy =>
                    {   
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                    });
                });

                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
                services.AddAutoMapper(typeof(MappingProfiles).Assembly);
                services.AddFluentValidationAutoValidation();
                services.AddValidatorsFromAssemblyContaining<Create>();
        
            return services;
        }
    }
}