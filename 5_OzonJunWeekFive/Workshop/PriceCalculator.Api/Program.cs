using FluentValidation.AspNetCore;
using PriceCalculator.Api.NamingPolicies;
using PriceCalculator.Bll;
using PriceCalculator.Dal;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
    });
services.AddEndpointsApiExplorer();

// add swagger
services.AddSwaggerGen(o =>
    {
     o.CustomSchemaIds(x => x.FullName);
    });

services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = true;
});

services
    .AddBll()
    .AddDalInfrastructure(builder.Configuration)
    .AddDalRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MigrateUp();
app.Run();

