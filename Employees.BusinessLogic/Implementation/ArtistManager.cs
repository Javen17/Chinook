using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using System;
using System.Collections.Generic;
using System.Text;

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
