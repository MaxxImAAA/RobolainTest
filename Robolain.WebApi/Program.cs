using Robolain.DAL.DependencyInjections;
using Robolain.Application.DependencyInjections;
using Robolain.WebApi.Middleware;
namespace Robolain.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDAL(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionValidationMiddleware();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
