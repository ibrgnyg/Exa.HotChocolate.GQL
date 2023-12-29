using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exa.Core.ApplicationStartup
{
    public interface IApplicationStartup
    {
        void ConfigureServices(IServiceCollection _services, IConfiguration _configuration);
        void Configure(IApplicationBuilder _application, IWebHostEnvironment _hostingEnvironment);
        bool IncludeService { get; }
    }
}
