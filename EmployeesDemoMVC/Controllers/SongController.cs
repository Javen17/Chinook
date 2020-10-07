using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChinookDemoMVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : BaseController<Song>
    {
        private readonly IBaseManager<Album> _albumManager;
        private readonly IBaseManager<Genre> _genreManager;
        public SongController(IObjectFactory factory) : base()
        {
            _manager = factory.Resolve<ISongManager>();
            _albumManager = factory.Resolve<IAlbumManager>();
            _genreManager = factory.Resolve<IGenreManager>();
        }


        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = _defaultPageSize)
        {
            var query = _manager.List();
            var itemCount = query.Count();
            int? pages = GetPages(itemCount, page, pageSize);

            query = Paginate(query, page);
            var items = query.ToList();

            ViewData["ListItems"] = items;
            ViewData["Pages"] = pages;
            ViewData["CurrentPage"] = page;
            return View("~/Views/Song/Index.cshtml");
        }

        [Route("[action]")]
        public IActionResult Create()
        {
           
            var query = _genreManager.List();
            var queryAlbum = _albumManager.List();
            var genres =      query.ToList();
            var albumItems =   queryAlbum.ToList();
            ViewData["GenreItems"] = genres;
            ViewData["AlbumItems"] = albumItems;
            return View("~/Views/Song/Create.cshtml");
        }
    }
}
