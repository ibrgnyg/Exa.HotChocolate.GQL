using Exa.Bussnies.Services.Interfaces;
using Exa.Configure.Models.BaseGraphQL;
using Exa.Configure.Models.DTOS;
using Exa.Domain.Models;
using HotChocolate.Types;

namespace Exa.HotChocolate.GQL.Api.GraphQL.Queries
{
    [ExtendObjectType(nameof(BaseQuery))]
    public class ProductQuery
    {
        private readonly IProductService _productService;

        public ProductQuery(IProductService productService)
        {
            _productService = productService;
        }

        public DTOPagination<Product> GetProducts(int activePage = 1, int pageSize = 10)
        {
            return _productService.GetDTOPagination(x => true, activePage, pageSize);
        }

        public Product GetProduct(string id)
        {
            return _productService.GetProduct(id);
        }
    }
}
