using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChinookDemoMVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : BaseController<Artist>
    {
        public ArtistController(IObjectFactory factory) : base()
        {
            _manager = factory.Resolve<IArtistManager>();
        }
         
        [HttpGet]
         public IActionResult Index()
        {
            var artist = new Artist();
            artist.Name = "Mika";
            _manager.Add(artist);
            return View("~/Views/Crud/Artist.cshtml");
        }

    }
}
