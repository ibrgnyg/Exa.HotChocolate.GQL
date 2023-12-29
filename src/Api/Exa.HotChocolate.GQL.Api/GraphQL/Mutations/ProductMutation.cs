using Exa.Bussnies.Services.Interfaces;
using Exa.Configure.Models.BaseGraphQL;
using Exa.Configure.Models.Configure;
using Exa.Configure.Models.DTOS;
using HotChocolate.Types;

namespace Exa.HotChocolate.GQL.Api.GraphQL.Mutations
{
    [ExtendObjectType(nameof(BaseMutation))]
    public class ProductMutation
    {
        private readonly IProductService _productService;

        public ProductMutation(IProductService productService)
        {
            _productService = productService;
        }

        public QLResult AddProduct(DTOProduct model)
        {
            return _productService.SaveProduct(model);
        }

        public QLResult UpdateProduct(DTOProduct model)
        {
            return _productService.UpdateProduct(model);
        }

        public QLResult DeleteProduct(string id)
        {
            return _productService.RemoveProduct(id);
        }
    }

}
