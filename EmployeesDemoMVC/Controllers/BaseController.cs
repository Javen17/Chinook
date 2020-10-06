using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChinookDemoMVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Chinook.BusinessLogic;
    using Chinook.BusinessLogic.Interface;
    using Chinook.BusinessModel.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace FoxyPos.Api.Controllers
    {
        public abstract class BaseController<TEntity> : Controller where TEntity : KeyedEntity, new()
        {
            public IBaseManager<TEntity> _manager;
            public const int _defaultPageSize = 5;

            [HttpGet("json")]
            public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get(int? page, int pageSize = _defaultPageSize)
            {
                try
                {
                    IQueryable<TEntity> query = _manager.List();
                    var itemCount = query.Count();
                    int? pages = GetPages(itemCount, page, pageSize);

                    if (page.HasValue)
                        query = Paginate(query, page.Value);

                    var items = query.ToList();

                    return Ok(new
                    {
                        Total = itemCount,
                        Pages = pages,
                        Data = items
                    });

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }
            }

            [HttpGet("json/{id}")]
            public virtual async Task<ActionResult<TEntity>> Get(int id)
            {
                try
                {
                    var item = await _manager.Get(id);

                    if (item == null)
                    {
                        return NotFound();
                    }

                    return item;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }
            }

            [HttpPut("json/{id}")]
            public virtual async Task<IActionResult> PutJson(long id, TEntity item)
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

                    return Ok();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }

            }

            [HttpPost]
            public virtual async Task<ActionResult<TEntity>> Post([FromForm] TEntity item)
            {
                try
                {
                    await _manager.Add(item);
                    return CreatedAtAction("Post", new { id = item.Key }, item);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }
            }

            [HttpPost("json/")]
            public virtual async Task<ActionResult<TEntity>> PostJson(TEntity item)
            {
                try
                {
                    await _manager.Add(item);
                    return CreatedAtAction("Post", new { id = item.Key }, item);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }
            }

            [HttpDelete("json/{id}")]
            public virtual async Task<ActionResult<TEntity>> DeleteJson(int id)
            {
                try
                {
                    var item = await _manager.Delete(id);
                    if (item == null)
                    {
                        return NotFound();
                    }

                    return Ok();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }

            }


            ///TODO possibly issue where method doesnt search and returns all values, if the search arguments are not valid return 400 not a 
            [HttpGet("search/")]
            public virtual async Task<ActionResult<TEntity>> Search(string filterField = null, string filterValue = null, string sortProperty = null, int? page = null, int pageSize = _defaultPageSize)
            {
                Debug.WriteLine(filterField);
                Debug.WriteLine(filterValue);

                var query = _manager.Search(filterField, filterValue, sortProperty);
                var itemCount = query.Count();
                int? pages = GetPages(itemCount, page, pageSize);

                if (page.HasValue)
                    query = Paginate(query, page.Value);

                var items = query.ToList();

                return Ok(new
                {
                    total = itemCount,
                    pages = pages,
                    data = items
                });
            }


            ///maybe we could make this a mode of search and not a different endpoint
            [HttpGet("search-all/")]
            public virtual async Task<ActionResult<TEntity>> SearchAllFields(string filterValue = null, string sortProperty = null, int? page = null, int pageSize = _defaultPageSize)
            {
                try
                {
                    var query = _manager.SearchAll(filterValue, sortProperty);
                    var itemCount = query.Count();
                    int? pages = GetPages(itemCount, page, pageSize);

                    if (page.HasValue)
                        query = Paginate(query, page.Value);

                    var items = query.ToList();

                    return Ok(new
                    {
                        total = itemCount,
                        pages = pages,
                        data = items
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    LogManager.Current.Log.Error(e);
                    return StatusCode(500, "Internal Server Error " + e.Message);
                }
            }


            [NonAction]
            public IQueryable<TEntity> Paginate(IQueryable<TEntity> query, int page, int pageSize = _defaultPageSize)
            {
                return query.Skip((page - 1) * pageSize).Take(pageSize);

            }

            [NonAction]
            public int? GetPages(int itemCount, int? page, int pageSize)
            {
                int? pages = null;

                if (page.HasValue && pageSize > 0)
                {
                    if (itemCount == 0)
                        pages = 0;
                    else
                        pages = itemCount / pageSize + (itemCount % pageSize != 0 ? 1 : 0);
                }

                return pages;
            }

            [NonAction]
            public async virtual Task<string> AddImage(IFormFile file, string rootPath, string folderName = "Uploads/")
            {
                string fileName = Path.GetFileName(file.FileName);
                string uploads = Path.Combine(rootPath, folderName);
                string filePath = Path.Combine(uploads, file.FileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    //fileStream.Close();
                }

                return Path.Combine(folderName, file.FileName);

            }


        }
    }
}
