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
    public class EmployeeManager : BaseManager<Employee>, IEmployeeManager
    {
        public EmployeeManager(ChinookContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Employee;
        }

        public Employee GetIncluded(long id)
        {
            return _dbSet.Include(e => e.DirectBoss).FirstOrDefault(p => p.EmployeeId == id);
        }

        public IQueryable<Employee> ListIncluded()
        {
            return _dbSet.Include(e => e.DirectBoss);
        }
    }
}
