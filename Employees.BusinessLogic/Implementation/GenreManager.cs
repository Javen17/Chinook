using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chinook.BusinessLogic.Implementation
{
    public class GenreManager : BaseManager<Genre>, IGenreManager
    {
        public GenreManager(ChinookContext context): base(context)
        {
            _context = context;
            _dbSet = _context.Genre;
        }

        

    }
}
