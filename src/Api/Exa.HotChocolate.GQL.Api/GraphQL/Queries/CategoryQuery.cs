using Exa.Bussnies.Services.Interfaces;
using Exa.Configure.Models.BaseGraphQL;
using Exa.Configure.Models.DTOS;
using Exa.Domain.Models;
using HotChocolate.Types;

namespace Exa.HotChocolate.GQL.Api.GraphQL.Queries
{
    [ExtendObjectType(nameof(BaseQuery))]
    public class CategoryQuery
    {
        private readonly ICategoryService _CategoryService;

        public CategoryQuery(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        public DTOPagination<Category> GetCategories(int activePage = 1, int pageSize = 10)
        {
            return _CategoryService.GetDTOPagination(x => true, activePage, pageSize);
        }

        public Category GetCategory(string id)
        {
            return _CategoryService.GetCategory(id);
        }
    }
}
