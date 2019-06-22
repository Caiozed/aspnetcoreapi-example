using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Business;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tapioca.HATEOAS;

namespace AspNetCoreApiExample.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookBusiness _booksBusiness;

        public BooksController(IBookBusiness booksBusiness)
        {
            _booksBusiness = booksBusiness;
        }
        // GET: api/Books
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<BookVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get()
        {
            return Ok(_booksBusiness.FindAll());
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        [SwaggerResponse(200, Type = typeof(BookVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get(int id)
        {
            BookVO book = _booksBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(BookVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_booksBusiness.Create(book));
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [SwaggerResponse(200, Type = typeof(BookVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Put(int id, [FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_booksBusiness.Update(book));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Delete(int id)
        {
            if (_booksBusiness.Delete(id)) return NoContent();
            return NotFound();
        }
    }
}
