using Exa.Bussnies.Services.Interfaces;
using Exa.Configure.Models.BaseGraphQL;
using Exa.Configure.Models.Configure;
using Exa.Configure.Models.DTOS;
using HotChocolate.Types;

namespace Exa.HotChocolate.GQL.Api.GraphQL.Mutations
{
    [ExtendObjectType(nameof(BaseMutation))]
    public class CategoryMutation
    {
        private readonly ICategoryService _categoryService;

        public CategoryMutation(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public QLResult AddCategory(DTOCategory model)
        {
            return _categoryService.SaveCategory(model);
        }

        public QLResult UpdateCategory(DTOCategory model)
        {
            return _categoryService.UpdateCategory(model);
        }

        public QLResult DeleteCategory(string id)
        {
            return _categoryService.RemoveCategory(id);
        }
    }

}
