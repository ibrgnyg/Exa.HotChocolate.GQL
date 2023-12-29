using Exa.Configure.Models.Configure;
using Exa.Configure.Models.DTOS;
using Exa.Domain.Models;
using System.Linq.Expressions;

namespace Exa.Bussnies.Services.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> Table { get; }
        List<Category> GetAllCategory(Expression<Func<Category, bool>> filter = null,
            int limit = 10,
            int skip = 0);
        DTOPagination<Category> GetDTOPagination(Expression<Func<Category, bool>> filter = null, int activePage = 1, int pageSize = 10);
        Category GetCategory(string id);
        Category GetCategoryFilter(Expression<Func<Category, bool>> filter);
        long GetCountCategory(Expression<Func<Category, bool>> filter);
        bool ExistCategory(Expression<Func<Category, bool>> filter);
        QLResult SaveCategory(DTOCategory model);
        QLResult UpdateCategory(DTOCategory model);
        QLResult UpdateOneFieldCategory<U>(string id, Expression<Func<Category, U>> expression, U value);
        QLResult RemoveCategory(string id);
    }
}
