using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChinookDemoMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : BaseController<Album>
    {
     
        public AlbumController(IObjectFactory factory) : base()
        {
            _manager = factory.Resolve<IAlbumManager>();
        }

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
            return View("~/Views/Album/Index.cshtml");
        }

        [HttpDelete]
        public async Task<ActionResult<Genre>> Delete(int id)
        {
            try
            {
                var item = await _manager.Delete(id);
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
