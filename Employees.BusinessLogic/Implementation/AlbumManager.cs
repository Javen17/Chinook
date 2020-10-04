using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BusinessLogic.Implementation
{
    class AlbumManager : BaseManager<Album>, IAlbumManager
    {
        public AlbumManager(ChinookContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Album;
        }
    }
}
