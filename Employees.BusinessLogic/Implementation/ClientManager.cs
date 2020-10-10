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
    public class ClientManager : BaseManager<Client>, IClientManager
    {
        public ClientManager(ChinookContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Client;
        }

        public IQueryable<Client> ListIncluded()
        {
            return _dbSet.Include(c => c.Support);
        }
    }
}
