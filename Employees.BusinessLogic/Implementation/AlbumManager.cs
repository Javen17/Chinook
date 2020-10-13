using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Album GetWithArtists(long id)
        {
            return _dbSet.Include(a => a.Artists).FirstOrDefault(a => a.AlbumId == id);
        }

        
    
    }
}
