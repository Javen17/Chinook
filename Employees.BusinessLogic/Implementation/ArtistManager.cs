using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;

namespace Chinook.BusinessLogic.Implementation
{
    public class ArtistManager : BaseManager<Artist>, IArtistManager
    {
        public ArtistManager(ChinookContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Artist;
        }
    }
}
