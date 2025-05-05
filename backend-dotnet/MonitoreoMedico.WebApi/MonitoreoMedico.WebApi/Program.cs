
using MonitoreoMedico.WebApi.Hubs;
using MonitoreoMedico.WebApi.Services;

namespace MonitoreoMedico.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.  
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddHostedService<NatsListenerService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.UseRouting();
            app.UseCors("AllowFrontend");

            // Configure the HTTP request pipeline.  
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Replace UseEndpoints with top-level route registrations  
            app.MapHub<NatsHub>("/instrumentosHub");

            app.MapControllers();

            app.Run();
        }
    }
}
