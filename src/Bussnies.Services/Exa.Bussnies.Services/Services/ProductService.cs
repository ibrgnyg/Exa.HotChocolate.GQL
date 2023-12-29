using Exa.Bussnies.Services.Interfaces;
using Exa.Configure.Models.Configure;
using Exa.Configure.Models.DTOS;
using Exa.Configure.Models.Enums;
using Exa.Core.MongoDB;
using Exa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Bussnies.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoRepository<Product> _collectionRepository;
        private readonly QLResult _qLResult;
        private readonly ICategoryService _categoryService;

        public ProductService(
            IMongoRepository<Product> collectionRepository, ICategoryService categoryService)
        {
            _collectionRepository = collectionRepository;
            _categoryService = categoryService;
            _qLResult = new QLResult();
        }


        public IQueryable<Product> Table => _collectionRepository.Table;

        public bool ExistProduct(Expression<Func<Product, bool>> filter)
        {
            return _collectionRepository.Exists(filter);
        }

        public virtual List<Product> GetAllProduct(Expression<Func<Product, bool>> filter = null, int limit = 10, int skip = 0)
        {
            return _collectionRepository.GetListExtended(filter, limit, skip).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual Product GetProductFilter(Expression<Func<Product, bool>> filter)
        {
            var Product = _collectionRepository.FirstOrDefault(filter).ConfigureAwait(false).GetAwaiter().GetResult();
            return Product;
        }

        public DTOPagination<Product> GetDTOPagination(Expression<Func<Product, bool>> filter = null, int activePage = 1, int pageSize = 10)
        {
            var dTOPagination = new DTOPagination<Product>();

            var queryable = Table
                           .Where(filter)
                           .OrderByDescending(x => x.CreateDate)
                           .Skip((activePage - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();

            if (!queryable.Any() || queryable.Count == 0 || queryable == null)
            {
                return new DTOPagination<Product>();
            }

            int totalCount = (int)GetCountProduct(filter);
            double pageCount = (double)((decimal)totalCount / pageSize);

            foreach (var item in queryable)
            {
                if (string.IsNullOrEmpty(item.CategoryId))
                    continue;

                item.CategoryId = _categoryService.GetCategory(item.CategoryId).CategoryName;
            }

            return new DTOPagination<Product>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                ActivePage = activePage,
                Data = queryable,
                TotalPageCount = (int)Math.Ceiling(pageCount)
            };
        }

        public virtual Product GetProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            var Product = _collectionRepository.GetById(id).ConfigureAwait(false).GetAwaiter().GetResult();
            return Product;
        }

        public virtual long GetCountProduct(Expression<Func<Product, bool>> filter)
        {
            return (int)_collectionRepository.GetCount(filter).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual QLResult RemoveProduct(string id)
        {
            try
            {
                var model = GetProduct(id);
                if (model == null)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.NotFound);
                }

                var updateResult = _collectionRepository.Remove(id).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!updateResult.IsAcknowledged)
                {
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Error);
                }
                _qLResult.SetSuccessEvent("");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }

        public virtual QLResult SaveProduct(DTOProduct model)
        {
            try
            {
                if (model == null)
                    return _qLResult.SetErrorEvent("error_mes", QLResultType.Empty);

                var product = new Product()
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    Tags = model.Tags,
                };

                for (int i = 0; i < model.Images.Count; i++)
                {
                    product.Images.Add(new Domain.SubModels.Image()
                    {
                        Url = model.Images[i]
                    });
                }

                var result = _collectionRepository.Save(product).ConfigureAwait(false).GetAwaiter().GetResult();

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

        private Product MappingModel(Product current, DTOProduct dTOProduct)
        {
            current.ProductName = dTOProduct.ProductName;
            current.ProductDescription = dTOProduct.ProductDescription;
            current.CategoryId = dTOProduct.CategoryId;
            current.Price = dTOProduct.Price;
            current.Tags = dTOProduct.Tags;
            current.Images = new List<Domain.SubModels.Image>();

            for (int i = 0; i < dTOProduct.Images.Count; i++)
            {
                current.Images.Add(new Domain.SubModels.Image()
                {
                    Url = dTOProduct.Images[i]
                });
            }
            return current;
        }

        public virtual QLResult UpdateProduct(DTOProduct model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id))
                { }
                //return _eventResult.SetErrorEvent("");

                var getUpdatedValue = GetProduct(model.Id);

                if (getUpdatedValue == null)
                {
                    //return _eventResult.SetErrorEvent("");
                }
                var updatedValue = MappingModel(getUpdatedValue, model);

                var updateResult = _collectionRepository.Update(updatedValue).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!updateResult.IsAcknowledged)
                {
                    //return _eventResult.SetErrorEvent("");
                }
                //_eventResult.SetSuccessEvent("");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }

        public virtual QLResult UpdateOneFieldProduct<U>(string id, Expression<Func<Product, U>> expression, U value)
        {
            try
            {
                var updateResult = _collectionRepository.UpdateField(id, expression, value).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!updateResult.IsAcknowledged)
                {
                    return new QLResult(); //_eventResult.SetErrorEvent("");
                }
                //_eventResult.SetSuccessEvent("");
            }
            catch (Exception ex)
            {
                _qLResult.SetExceptionEvent(ex);
            }
            return _qLResult;
        }
    }
}
