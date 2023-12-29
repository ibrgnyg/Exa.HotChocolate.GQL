using Exa.Bussnies.Services.Interfaces;
using Exa.Bussnies.Services.Services;
using Exa.Configure.Models.Configure;
using Exa.Core.ApplicationStartup;
using Exa.Core.MongoDB;
using Microsoft.Extensions.Options;

namespace Exa.HotChocolate.GQL.Application
{
    public class ApplicationStartup : IApplicationStartup
    {
        public bool IncludeService => true;

        public void Configure(IApplicationBuilder _application, IWebHostEnvironment _hostingEnvironment)
        {
            _application.UseStaticFiles();
        }

        public void ConfigureServices(IServiceCollection _services, IConfiguration _configuration)
        {
            //DB
            _services.Configure<DBSettings>(_configuration.GetSection(nameof(DBSettings)));
            _services.AddSingleton<IDBSettings>(_ => _.GetRequiredService<IOptions<DBSettings>>().Value);
            _services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            //Services
            _services.AddSingleton<ICategoryService, CategoryService>();
            _services.AddSingleton<IProductService, ProductService>();
        }
    }
}
