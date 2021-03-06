using Api.StartupConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Telegram.Bot;
using TelegramBot;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureDbContext();
            services.ConfigureMediator();
            services.ConfigureTelegramBot(Configuration["TelegramBot:Token"]);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TelegramBotClient botClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.ApplicationServices.ConfigureTelegramBot(botClient);
            app.ApplicationServices.ConfigureDbContext();
        }
    }
}
