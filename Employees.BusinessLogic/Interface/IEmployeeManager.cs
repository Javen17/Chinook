using Chinook.BusinessModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chinook.BusinessLogic.Interface
{
    public interface IEmployeeManager : IBaseManager<Employee>
    {
        IQueryable<Employee> ListIncluded();
        Employee GetIncluded(long id);
    }
}
