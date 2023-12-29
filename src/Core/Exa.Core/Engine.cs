using Exa.Core.ApplicationStartup;
using Exa.Core.TypeFinders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exa.Core
{
    public class Engine
    {
        public static WebApplicationBuilder CreateAsBuilder(string[] args)
        {
            var application = WebApplication.CreateBuilder(args);
            return application;
        }

        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.BuildServiceProvider();
        }

        public static void ConfigureRequestPipeline(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
        {
            var typeFinder = new AppTypeFinder();
            var getstartupConfigurations = typeFinder.FindClassesOfType<IApplicationStartup>();

            var instances = getstartupConfigurations
                    .Select(startup => Activator.CreateInstance(startup) as IApplicationStartup)
                    .Where(startup => startup.IncludeService);

            foreach (var instance in instances)
                instance?.Configure(application, webHostEnvironment);
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var typeFinder = new AppTypeFinder();
            typeFinder.EnsureBinFolderAssembliesLoaded = true;
            var startupConfigurations = typeFinder.FindClassesOfType<IApplicationStartup>();

            var instances = startupConfigurations
                 .Select(startup => Activator.CreateInstance(startup) as IApplicationStartup)
                 .Where(startup => startup.IncludeService);

            foreach (var instance in instances)
                instance?.ConfigureServices(services, configuration);
        }

        public static void ConfigurationFiles(IConfigurationBuilder configurationBuilder)
        {
            string? directoryBasePath = Directory.GetParent(AppContext.BaseDirectory)?.FullName;

            bool alwaysReload = true, alwaysOptionIncluded = true;

            configurationBuilder.SetBasePath(directoryBasePath)
                .AddJsonFile("configure.json", alwaysOptionIncluded, alwaysReload)
                .AddJsonFile("bussnies.configure.json", alwaysOptionIncluded, alwaysReload)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
