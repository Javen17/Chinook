using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using EmployeesBusinessModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoweredSoft.DynamicLinq;

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

        public override IQueryable<Album> SearchAll(string filterValue, string sortProperty)
        {
            IQueryable<Album> query = _dbSet.AsQueryable();
            string[] ignoredFields = new string[] {"Name"};

            query = CreateSearchQuery<Album>(query, filterValue, ignoredFields);

            if (sortProperty != null)
                query.OrderBy(sortProperty);

            return query;
        }

    }
}
