
using Microsoft.AspNetCore.Mvc;
using Chinook.BusinessModel.Models;
using ChinookDemoMVC.Controllers.FoxyPos.Api.Controllers;
using Chinook.BusinessLogic;
using Chinook.BusinessLogic.Interface;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

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
            return View("~/Views/Genre/Index.cshtml");
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
                return View("~/Views/Genre/Index.cshtml");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                LogManager.Current.Log.Error(e);
                return StatusCode(500, "Internal Server Error " + e.Message);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put(long id, [FromForm]Genre item)
        {
            try
            {
                if (id != item.Key)
                {
                    return BadRequest();
                }

                bool result = await _manager.Modify(id, item);

                if (result == false)
                {
                    return NotFound();
                }

                ViewData["Modified"] = true;
                return View("~/Views/Genre/Index.cshtml");
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
