
using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChinookDemoMVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IObjectFactory factory) : base()
        {
            _employeeManager = factory.Resolve<IEmployeeManager>();
            _manager = factory.Resolve<IEmployeeManager>();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            var employees = _employeeManager.List().ToList();
            ViewData["Employees"] = employees;

            return View("~/Views/Employee/Create.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Store(IFormCollection form)
        {
            var employee = new Employee();
            employee.Name = form["Name"].ToString();
            employee.LastName = form["LastName"].ToString();
            employee.Email = form["Email"].ToString();
            employee.Phone = form["Phone"].ToString();
            employee.JobPosition = form["JobPosition"].ToString();

            Console.WriteLine("DirectBoss: " + form["DirectBoss"].ToString());

            if (form["DirectBoss"].ToString() != "")
            {
                employee.DirectBossEmployeeId = long.Parse(form["DirectBoss"].ToString());
            }
            
            await _employeeManager.Add(employee);

            return Redirect("/Employee");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(long id)
        {
            var employee = _employeeManager.Get(id).Result;
            var employeeItems = _employeeManager.List().ToList();

            if (employee == null)
            {
                return Redirect("/Employee");
            }

            ViewData["Current"] = employee;
            ViewData["Employees"] = employeeItems;

            return View("~/Views/Employee/Edit.cshtml");

        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Change(IFormCollection form)
        {
            var employee = new Employee();
            employee.EmployeeId = long.Parse(form["Id"].ToString());
            employee.Name = form["Name"].ToString();
            employee.LastName = form["LastName"].ToString();
            employee.Email = form["Email"].ToString();
            employee.Phone = form["Phone"].ToString();
            employee.JobPosition = form["JobPosition"].ToString();

            Console.WriteLine("DirectBoss: " + form["DirectBoss"].ToString());

            if(form["DirectBoss"].ToString() != "")
            {
                employee.DirectBossEmployeeId = long.Parse(form["DirectBoss"].ToString());
            }

            await _employeeManager.Modify(employee.EmployeeId, employee);

            return Redirect("/Employee");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public virtual async Task<IActionResult> Remove(long id)
        {
            await _employeeManager.Delete(id);
            return Redirect("/Employee");
        }
    }
}
