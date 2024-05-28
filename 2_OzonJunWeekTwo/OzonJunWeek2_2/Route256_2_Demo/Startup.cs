using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Route256_2_Demo.Middleware;

namespace Route256_2_Demo
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDeliveryCalculator, DeliveryCalculator>(x => new DeliveryCalculator(
                String.Empty, 1, String.Empty));
            services.AddScoped<PriceCalculationService>();
            var serviceProvider = services.BuildServiceProvider();
            var calculator = serviceProvider.GetRequiredService<IDeliveryCalculator>();
            services.Configure<Config>(Configuration.GetSection("Config"));

            services.AddTransient<TransientService>();
            services.AddScoped<ScopedService>();
            services.AddSingleton<SingltonService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Route256_2_Demo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<Config> optionsConfig,
            IOptionsSnapshot<Config> optionsSnapshot, IOptionsMonitor<Config> optionsMonitor,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Route256_2_Demo v1"));
            }

            app.UseRouting();

            app.UseMyMiddleware(logger);
            app.UseMiddleware<MyMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",  async context =>
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder
                        .AppendLine($"Text, {_environment.EnvironmentName}")
                        .AppendLine($"ConfigValue, {Configuration.GetValue<string>("ConfigValue")}")
                        .AppendLine($"Config, {Configuration.GetValue<string>("Config:Value")}")
                        .AppendLine($"Config.Value from Object, {Configuration.GetSection("Config").Get<Config>().ConfigValue}")
                        .AppendLine($"IOptions, {optionsConfig.Value.ConfigValue}")
                        .AppendLine($"IOptionsSnapshot, {optionsSnapshot.Value.ConfigValue}")
                        .AppendLine($"IOptionsMonitor, {optionsMonitor.CurrentValue.ConfigValue}")
                        .AppendLine($"IOptionsMonitor.SecretValue, {optionsMonitor.CurrentValue.SecretValue}");
                    await context.Response.WriteAsync(stringBuilder.ToString());
                });
                endpoints.MapControllers();
            });
        }
    }

    public class Config
    {
        public string ConfigValue { get; set; }
        public string SecretValue { get; set; }
    }
}