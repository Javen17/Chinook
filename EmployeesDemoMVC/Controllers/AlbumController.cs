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

namespace ChinookDemoMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : BaseController<Album>
    {

        private readonly IAlbumManager albumManager;
        private readonly IArtistManager artistManager;
        public AlbumController(IObjectFactory factory) : base()
        {
            _manager = factory.Resolve<IAlbumManager>();
            albumManager = factory.Resolve<IAlbumManager>();
            artistManager = factory.Resolve<IArtistManager>();
            _mainCrudView = "~/Views/Album/Index.cshtml";
        }

        [HttpGet]
        override
        public IActionResult Index(int page = 1, int pageSize = _defaultPageSize)
        {
            var query = albumManager.List();
            var itemCount = query.Count();
            int? pages = GetPages(itemCount, page, pageSize);

            query = Paginate(query, page);
            var items = query.ToList();

            ViewData["ListItems"] = items;
            ViewData["Pages"] = pages;
            ViewData["CurrentPage"] = page;
            return View("~/Views/Album/Index.cshtml");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(long id)
        {
            var album = albumManager.GetWithArtists(id);

            if(album == null)
            {
                return Redirect("/Album");
            }

            ViewData["Album"] = album;

            return View("~/Views/Album/Edit.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> AddArtist(IFormCollection form)
        {
            var artist = new Artist();
            artist.AlbumId = long.Parse(form["AlbumId"].ToString());
            artist.Name = form["Name"].ToString();

            await artistManager.Add(artist);

            return Redirect("/Album/Edit/" + artist.AlbumId);

        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Store(IFormCollection form)
        {
            var album = new Album();
            album.Title = form["Title"].ToString();
            
            await albumManager.Add(album);

            return Redirect("/Album");

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult RemoveArtist(long id)
        {
            var artist = artistManager.Get(id).Result;

            artistManager.Delete(id);

            return Redirect("/Album/Edit/" + artist.AlbumId);
        }

        [HttpDelete]
        public async Task<ActionResult<Genre>> Delete(int id)
        {
            try
            {
                var item = await albumManager.Delete(id);
                if (item == null)
                {
                    return NotFound();
                }

                ViewData["DeletedItem"] = item;
                ViewData["Deleted"] = true;
                return View("~/Views/Album/Index.cshtml");
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
