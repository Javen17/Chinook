using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Chinook.BusinessLogic.Implementation
{
    public class SongManager: BaseManager<Song>, ISongManager
    {
        public SongManager(ChinookContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Song;
        }

        public IQueryable<Song> GetIncluded()
        {
            return _dbSet.Include(m => m.Album).Include(m => m.Genre);
        }

        public IQueryable<Song> SearchAllIncluded(string filterValue, string sortProperty)
        {
            IQueryable<Song> query = _dbSet.AsQueryable().Include(m => m.Album).Include(m => m.Genre);
            string[] ignoredFields = new string[] { };

            query = CreateSearchQuery(query, filterValue, ignoredFields);

            /*if (sortProperty != null)
                query.OrderBy(sortProperty); */

            return query;
        }
    }
}
