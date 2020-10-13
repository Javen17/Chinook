using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChinookDemoMVC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : BaseController<Song>
    {
        private readonly IBaseManager<Album> _albumManager;
        private readonly IBaseManager<Genre> _genreManager;
        private readonly ISongManager _songManager;
        private readonly ILogger<SongController> _logger;
        public SongController(IObjectFactory factory, ILogger<SongController> logger) : base()
        {
            //_manager = factory.Resolve<ISongManager>();
            _albumManager = factory.Resolve<IAlbumManager>();
            _genreManager = factory.Resolve<IGenreManager>();
            _songManager = factory.Resolve<ISongManager>();
            _logger = logger;
            _manager = factory.Resolve<ISongManager>();
            _mainCrudView = "~/Views/Song/Index.cshtml";
        }


        [HttpGet]
        override
        public IActionResult Index(int page = 1, int pageSize = _defaultPageSize)
        {
            var query = _songManager.GetIncluded();
            var itemCount = query.Count();
            int? pages = GetPages(itemCount, page, pageSize);

            query = Paginate(query, page);
            var items = query.ToList();

            ViewData["ListItems"] = items;
            ViewData["Pages"] = pages;
            ViewData["CurrentPage"] = page;
            return View("~/Views/Song/Index.cshtml");
        }

        [HttpGet]
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

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Store(IFormCollection collection)
        {
            var song = new Song();
            song.Name = collection["Name"].ToString();
            long albumId = long.Parse(collection["Album"].ToString());
            long genreId = long.Parse(collection["Genre"].ToString());

            //song.Album = _albumManager.Get(albumId).Result;
            //song.Genre = _genreManager.Get(genreId).Result;
            song.AlbumId = albumId;
            song.GenreId = genreId;

            /*song.AlbumId = albumId;
            song.GenreId = genreId;*/

            Console.WriteLine("AlbumId: " + song.AlbumId);
            Console.WriteLine("GenreId: " + song.GenreId);

            

            await _songManager.Add(song);


            return Redirect("/Song");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(long id)
        {
            var query = _genreManager.List();
            var queryAlbum = _albumManager.List();
            var genres = query.ToList();
            var albumItems = queryAlbum.ToList();

            var song = _songManager.Get(id).Result;

            ViewData["GenreItems"] = genres;
            ViewData["AlbumItems"] = albumItems;
            ViewData["CurrentSong"] = song;
            return View("~/Views/Song/Edit.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Change(IFormCollection form)
        {
            long id = long.Parse(form["Id"].ToString());
            var song = new Song();

            song.Name = form["Name"].ToString();
            song.AlbumId = long.Parse(form["Album"].ToString());
            song.GenreId = long.Parse(form["Genre"].ToString());
            song.SongId = id;

            await _songManager.Modify(id, song);

            return Redirect("/Song");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public virtual async Task<IActionResult> Remove(long id)
        {

            await _songManager.Delete(id);
            return Redirect("/Song");
        }

        [HttpGet("search-all/")]
        override
        public async Task<ActionResult<Song>> SearchAllFields(string filterValue = null, string sortProperty = null, int? pageNumber = 1, int pageSize = _defaultPageSize)
        {
            try
            {
                var query = _songManager.SearchAllIncluded(filterValue, sortProperty);
                var itemCount = query.Count();
                int? pages = GetPages(itemCount, pageNumber, pageSize);

                if (pageNumber.HasValue)
                    query = Paginate(query, pageNumber.Value);

                var items = query.ToList();

                ViewData["ListItems"] = items;
                ViewData["Pages"] = pages;
                ViewData["CurrentPage"] = pageNumber;


                return View("~/Views/Song/Index.cshtml");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                LogManager.Current.Log.Error(e);
                return StatusCode(500, "Internal Server Error " + e.Message);
            }
        }
    }
}
