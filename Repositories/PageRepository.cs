using Core;
using Core.Exceptions;
using Core.Repositories;
using Idata.Data;
using Ihelpers.Helpers;
using Microsoft.EntityFrameworkCore;
using Page.Data;
using Page.Repositories.Interfaces;
using System.Linq.Dynamic.Core;

namespace Page.Repositories
{
    public class PageRepository : RepositoryBase<Idata.Data.Entities.Page.Page>, IPageRepository
    {
        private readonly ILogger<PageRepository>? Logger;

        private const string controllerName = "Page";
        public PageRepository(IdataContext dataContext, ILogger<PageRepository>? logger = null) 
        {
            //Dependency injection of dataContext and logger
            Logger = logger;
        }



        public override async Task<Idata.Data.Entities.Page.Page?> GetItem(UrlRequestBase? requestBase)
        {
            Idata.Data.Entities.Page.Page? model = null;//object to be returned
            try
            {
                var query = _dataContext.Page as IQueryable<Idata.Data.Entities.Page.Page>;
                //Try get the search filter
                string field = requestBase.GetFilter("field");

                bool isNumeric = await TypeHelper.isNumericValue(requestBase.criteria);

                if (isNumeric == false)
                {
                    query = _dataContext.Page.Where($"obj => obj.type == @0", requestBase.criteria);

                }
                else
                {
                    query = _dataContext.Page.Where($"obj => obj.{field} == @0", requestBase.criteria);
                }
                //Create base query based on criteria and field


                // TODO inyect the include parameters as given in front
                query = requestBase.GetIncludes(query);

                // get the model with given criteria
                model = await query.FirstOrDefaultAsync();


                //if model is null(not found) throw exception
                if (model == null) throw new ExceptionBase($"Item with id {field} {requestBase.criteria} not found", 404);
                // if the model is valid return the item
                return model;
            }
            catch (Exception ex)
            {
                ExceptionBase.HandleException(ex, $"Error creating {controllerName}");
            }
            return model;
        }
    }
}