using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;

namespace Chinook.BusinessLogic.Implementation
{
    public class SongManager: BaseManager<Song>, ISongManager
    {
        public SongManager(ChinookContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Song;
        }
    }
}
