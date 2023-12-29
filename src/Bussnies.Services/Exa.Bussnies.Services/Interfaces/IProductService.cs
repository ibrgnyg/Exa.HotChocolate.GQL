using Exa.Configure.Models.Configure;
using Exa.Configure.Models.DTOS;
using Exa.Domain.Models;
using System.Linq.Expressions;

namespace Exa.Bussnies.Services.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> Table { get; }
        List<Product> GetAllProduct(Expression<Func<Product, bool>> filter = null,
            int limit = 10,
            int skip = 0);
        DTOPagination<Product> GetDTOPagination(Expression<Func<Product, bool>> filter = null, int activePage = 1, int pageSize = 10);
        Product GetProduct(string id);
        Product GetProductFilter(Expression<Func<Product, bool>> filter);
        long GetCountProduct(Expression<Func<Product, bool>> filter);
        bool ExistProduct(Expression<Func<Product, bool>> filter);
        QLResult SaveProduct(DTOProduct model);
        QLResult UpdateProduct(DTOProduct model);
        QLResult UpdateOneFieldProduct<U>(string id, Expression<Func<Product, U>> expression, U value);
        QLResult RemoveProduct(string id);
    }
}
