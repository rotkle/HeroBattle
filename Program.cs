using HeroBattle.Helpers;
using HeroBattle.Models;
using HeroBattle.Services;
using HeroBattle.Services.Interfaces;

namespace HeroBattle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<DataContext>();

            // Add services to the container.
            builder.Services.AddScoped<IArenaService, ArenaService>();
            builder.Services.AddScoped<IBattleService, BattleService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            app.Run("http://localhost:4500");
        }
    }
}