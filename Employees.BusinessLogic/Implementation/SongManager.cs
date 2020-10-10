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
    }
}
