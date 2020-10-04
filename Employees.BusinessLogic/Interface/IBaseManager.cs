using Chinook.BusinessModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.BusinessLogic.Interface
{
    public interface IBaseManager<T> where T : KeyedEntity
    {
        IQueryable<T> List();

        Task<T> Get(long id);

        Task<T> Add(T model);

        Task<T> Delete(long id);

        Task<bool> Modify(long id, T entity);

        IQueryable<T> Search(string filterField, string filterValue, string sortProperty);

        IQueryable<T> SearchAll(string filterValue, string sortProperty);

        IQueryable<T> CreateSearchQuery<T>(IQueryable<T> db_set, string value, string[] ignoredFields, bool searchRelations = false, List<string> relatedList = null, bool caseSensitive = false) where T : class;

    }
}
