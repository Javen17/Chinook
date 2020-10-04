using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PoweredSoft.DynamicLinq;

namespace Chinook.BusinessLogic.Implementation
{
    public class
        BaseManager<TEntity> : IBaseManager<TEntity> where TEntity : KeyedEntity
    {
        public ChinookContext _context;
        public DbSet<TEntity> _dbSet;

        public BaseManager(ChinookContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> List()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task<TEntity> Get(long id)
        {
            List<Type> typeList = new List<Type>();

            typeList.Add(typeof(short));
            typeList.Add(typeof(int));
            typeList.Add(typeof(long));
            typeList.Add(typeof(byte));

            return await FindEntityTestType(id, typeList);
        }

        public virtual async Task<TEntity> Add(TEntity model)
        {
            _dbSet.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public virtual async Task<bool> Modify(long id, TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public virtual async Task<TEntity> Delete(long id)
        {
            List<Type> typeList = new List<Type>
            {
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(byte)
            };

            TEntity entity = await FindEntityTestType(id, typeList);


            if (entity == null)
            {
                return null;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        ///maybe could be done better
        protected virtual async Task<TEntity> FindEntityTestType(long id, List<Type> typeList, int i = 0)
        {
            TEntity entity;

            try
            {
                var newId = Convert.ChangeType(id, typeList[i]);
                entity = await _dbSet.FindAsync(newId);
                return entity;
            }
            catch
            {
                i += 1;
                if (typeList.Count > i)
                    return await FindEntityTestType(id, typeList, i);
                else
                    throw;
            }

        }

        public bool EntityExists(long id)
        {
            return _dbSet.Any(e => e.Key == id);
        }

        public virtual IQueryable<TEntity> Search(string filterField, string filterValue, string sortProperty)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
                query = query.Query(t => t.Contains(filterField, filterValue));
            if (sortProperty != null)
                query.OrderBy(sortProperty);
            return query;
        }


        public virtual IQueryable<TEntity> SearchAll(string filterValue, string sortProperty)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            string[] ignoredFields = new string[] { };

            query = CreateSearchQuery<TEntity>(query, filterValue, ignoredFields);

            if (sortProperty != null)
                query.OrderBy(sortProperty);

            return query;
        }

        ///i did it, but was it worth it?
        public IQueryable<T> CreateSearchQuery<T>(IQueryable<T> db_set, string value, string[] ignoredFields, bool searchRelations = false, List<string> relatedList = null, bool caseSensitive = false) where T : class
        {
            IQueryable<T> query = db_set;

            List<Expression> expressions = new List<Expression>();

            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");

            MethodInfo containsMethod;
            containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            relatedList = relatedList ?? new List<string>();


            foreach (PropertyInfo prop in typeof(T).GetProperties().Where(x => (x.PropertyType == typeof(string) || x.PropertyType.IsClass) && !ignoredFields.Contains(x.Name)))
            {
                if (prop.PropertyType != typeof(string) && searchRelations == true)
                {
                    foreach (PropertyInfo internalProp in prop.GetType().GetProperties().Where(x => x.PropertyType == typeof(string)))
                    {
                        Expression newProp = Expression.PropertyOrField(parameter, prop.Name);

                        if (!caseSensitive)
                        {
                            value = value.ToLower();
                            newProp = Expression.Call(Expression.PropertyOrField(newProp, internalProp.Name), "ToLower", null);
                        }
                        else
                        {
                            newProp = Expression.PropertyOrField(newProp, internalProp.Name);
                        }

                        expressions.Add(BuildStringExpression<T>(newProp, parameter, containsMethod, value, caseSensitive));
                    }
                }
                else if (prop.PropertyType == typeof(string))
                {
                    Expression memberExpression;

                    if (!caseSensitive)
                    {
                        value = value.ToLower();
                        memberExpression = Expression.Call(Expression.PropertyOrField(parameter, prop.Name), "ToLower", null);
                    }
                    else
                    {
                        memberExpression = Expression.PropertyOrField(parameter, prop.Name);
                    }

                    expressions.Add(BuildStringExpression<T>(memberExpression, parameter, containsMethod, value, caseSensitive));
                }
            }

            if (expressions.Count == 0)
                return query;

            Expression or_expression = expressions[0];

            for (int i = 1; i < expressions.Count; i++)
            {
                or_expression = Expression.OrElse(or_expression, expressions[i]);
            }

            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(
                or_expression, parameter);

            return query.Where(expression);
        }


        private Expression BuildStringExpression<T>(Expression memberExpression, ParameterExpression parameter, MethodInfo containsMethod, string searchValue, bool caseSensitive = false) where T : class
        {
            ConstantExpression valueExpression = Expression.Constant(searchValue, typeof(string));
            MethodCallExpression containsExpression;
            containsExpression = Expression.Call(memberExpression, containsMethod, valueExpression);

            return containsExpression;
        }

    }
}

