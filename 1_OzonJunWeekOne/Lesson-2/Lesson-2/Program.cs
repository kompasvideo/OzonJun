using Lessons_2.Bll.Services;
using Lessons_2.Bll.Services.Interfaces;
using Lessons_2.Dal.Repositories;
using Lessons_2.Dal.Repositories.Interfaces;

namespace Lessons_2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ICalculationService, CalculationService>();
        builder.Services.AddSingleton<ICalculationHistoryRepository, CalculationHistoryRepository>();
        
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();
        app.Run();
    }
}
    

