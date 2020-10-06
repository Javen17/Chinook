
using Microsoft.AspNetCore.Mvc;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Microsoft.AspNetCore.Http;

namespace ChinookDemoMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : BaseController<Genre>
    {
        
        public GenreController(IObjectFactory factory) : base()
        {
            _manager = factory.Resolve<IGenreManager>();
        }
        // GET: GenreController
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Genre/Index.cshtml");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            return View("~/Views/Genre/Create.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Store(IFormCollection collection)
        {
            var genre = new Genre();
            if(collection["Name"].ToString() == null)
            {
                return View("~/Views/Genre/Create.cshtml");
            }

            genre.Name = collection["Name"].ToString();

            _manager.Add(genre);

            return View("~/Views/Genre/Index.cshtml");
        }


    }
}
