using Exa.Bussnies.Services.Interfaces;
using Exa.Configure.Models.Configure;
using Exa.Configure.Models.DTOS;
using Exa.Configure.Models.Enums;
using Exa.Core.MongoDB;
using Exa.Domain.Models;
using System.Linq.Expressions;

namespace Exa.Bussnies.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoRepository<Category> _collectionRepository;
        private readonly QLResult _qLResult;
        public CategoryService(IMongoRepository<Category> collectionRepository)
        {
            _collectionRepository = collectionRepository;
            _qLResult = new QLResult();
        }


        public IQueryable<Category> Table => _collectionRepository.Table;

        public bool ExistCategory(Expression<Func<Category, bool>> filter)
        {
            return _collectionRepository.Exists(filter);
        }

        public virtual List<Category> GetAllCategory(Expression<Func<Category, bool>> filter = null, int limit = 10, int skip = 0)
        {
            return _collectionRepository.GetListExtended(filter, limit, skip).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual Category GetCategoryFilter(Expression<Func<Category, bool>> filter)
        {
            var Category = _collectionRepository.FirstOrDefault(filter).ConfigureAwait(false).GetAwaiter().GetResult();
            return Category;
        }

        public virtual DTOPagination<Category> GetDTOPagination(Expression<Func<Category, bool>> filter = null, int activePage = 1, int pageSize = 10)
        {
            var queryable = Table
                 .Where(filter)
                 .OrderByDescending(x => x.CreateDate)
                 .Skip((activePage - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            if (!queryable.Any() || queryable.Count == 0 || queryable == null)
            {
                return new DTOPagination<Category>();
            }

            int totalCount = (int)GetCountCategory(filter);
            double pageCount = (double)((decimal)totalCount / pageSize);

            return new DTOPagination<Category>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                ActivePage = activePage,
                Data = queryable,
                TotalPageCount = (int)Math.Ceiling(pageCount)
            };
        }

        public virtual Category GetCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var Category = _collectionRepository.GetById(id).ConfigureAwait(false).GetAwaiter().GetResult();
            return Category;
        }

        public virtual long GetCountCategory(Expression<Func<Category, bool>> filter)
        {
            return (int)_collectionRepository.GetCount(filter).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual QLResult RemoveCategory(string id)
        {
            try
            {
                var model = GetCategory(id);
                if (model == null)
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.NotFound);

                var updateResult = _collectionRepository.Remove(id).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!updateResult.IsAcknowledged)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Error);
                }
                _qLResult.SetSuccessEvent("succes_mes");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }

        public virtual QLResult SaveCategory(DTOCategory model)
        {
            try
            {
                if (model == null)
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Empty);

                var category = new Category()
                {
                    CategoryName = model.CategoryName,
                };

                var result = _collectionRepository.Save(category).ConfigureAwait(false).GetAwaiter().GetResult();

                if (result == null)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Error);
                }
                _qLResult.SetSuccessEvent("succes_mes");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }

        private Category MappingModel(Category current, DTOCategory model)
        {
            current.CategoryName = model.CategoryName;
            return current;
        }

        public virtual QLResult UpdateCategory(DTOCategory model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id))
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Empty);

                var getUpdatedValue = GetCategory(model.Id);

                if (getUpdatedValue == null)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.NotFound);
                }
                var updatedValue = MappingModel(getUpdatedValue, model);

                var updateResult = _collectionRepository.Update(updatedValue).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!updateResult.IsAcknowledged)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Error);
                }
                _qLResult.SetSuccessEvent("succes_mes");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }

        public virtual QLResult UpdateOneFieldCategory<U>(string id, Expression<Func<Category, U>> expression, U value)
        {
            try
            {
                var updateResult = _collectionRepository.UpdateField(id, expression, value).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!updateResult.IsAcknowledged)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Error);
                }
                _qLResult.SetSuccessEvent("succes_mes");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }
    }
}
