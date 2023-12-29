using Exa.Configure.Models.BaseGraphQL;
using Exa.HotChocolate.GQL.Api.GraphQL.Mutations;
using Exa.HotChocolate.GQL.Api.GraphQL.Queries;
using Exa.HotChocolate.GQL.Api.GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace Exa.HotChocolate.GQL.Api
{
    public static class ConfigureServices
    {
        private const string GraphQLPath = "/exagql";

        public static IServiceCollection SetGraphQLInitialize(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddGraphQLServer()
                .AddQueryType<BaseQuery>()
                .AddMutationType<BaseMutation>()
                .AddType<EnumResultType>()
                .AddType<ProductMutation>()
                .AddType<ProductQuery>()
                .AddType<CategoryMutation>()
                .AddType<CategoryQuery>();

            return services;
        }

        public static WebApplication BuildRun(this WebApplication app)
        {
            app.UseRouting();

            app.MapGraphQL(GraphQLPath); //default GQL path value graphql

            app.Run();

            return app;
        }
    }
}
