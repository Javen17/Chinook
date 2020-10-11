
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
    public class ClientController : BaseController<Client>
    {
        private readonly IEmployeeManager _employeeManager;

        public ClientController(IObjectFactory factory) : base()
        {
            _manager = factory.Resolve<IClientManager>();
            _employeeManager = factory.Resolve<IEmployeeManager>();
            _mainCrudView = "~/Views/Client/Index.cshtml";
        }

      
        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            var employees = _employeeManager.List().ToList();
            ViewData["Employees"] = employees;

            return View("~/Views/Client/Create.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Store(IFormCollection form)
        {
            var client = new Client();
            client.Name = form["Name"].ToString();
            client.LastName = form["LastName"].ToString();
            client.Email = form["Email"].ToString();
            client.Phone = form["Phone"].ToString();

            Console.WriteLine("Support: " + form["Support"].ToString());

            if (form["Support"].ToString() != "")
            {
                client.SupportEmployeeId = long.Parse(form["Support"].ToString());
            }

            await _manager.Add(client);

            return Redirect("/Client");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(long id)
        {
            var client = _manager.Get(id).Result;
            var clientItems = _employeeManager.List().ToList();

            if (client == null)
            {
                return Redirect("/Client");
            }

            ViewData["Current"] = client;
            ViewData["Employees"] = clientItems;

            return View("~/Views/Client/Edit.cshtml");

        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Change(IFormCollection form)
        {
            var client = new Client();
            client.ClientId = long.Parse(form["Id"].ToString());
            client.Name = form["Name"].ToString();
            client.LastName = form["LastName"].ToString();
            client.Email = form["Email"].ToString();
            client.Phone = form["Phone"].ToString();

            Console.WriteLine("Support: " + form["Support"].ToString());

            if (form["Support"].ToString() != "")
            {
                client.SupportEmployeeId = long.Parse(form["Support"].ToString());
            }

            await _manager.Modify(client.ClientId, client);

            return Redirect("/Client");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public virtual async Task<IActionResult> Remove(long id)
        {
            await _manager.Delete(id);
            return Redirect("/Client");
        }
    }
}
