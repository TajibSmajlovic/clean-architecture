using Kinoteka.TicketManagement.Api.Services;
using Kinoteka.TicketManagement.Api.Utility;
using Kinoteka.TicketManagement.Application;
using Kinoteka.TicketManagement.Application.Contracts;
using Kinoteka.TicketManagement.Infrastructure;
using Kinoteka.TicketManagement.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Kinoteka.TicketManagement.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureService(this WebApplicationBuilder builder)

        {
            AddSwagger(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kinoteka API"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.MapControllers();

            return app;
        }

        public static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kinoteka API",
                });

                c.OperationFilter<FileResultContentTypeOperationFilter>();
            });
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<KinotekaDbContext>();

                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}