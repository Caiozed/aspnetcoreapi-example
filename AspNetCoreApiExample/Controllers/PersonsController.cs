using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Business;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreApiExample.Data.VO;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreApiExample.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        // GET api/values
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<IEnumerable<PersonVO>> Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(PersonVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<PersonVO> Get(long id)
        {
            PersonVO person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }


        // GET api/paersons/5
        [HttpGet("find-by-name")]
        [SwaggerResponse(200, Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<PersonVO> FindByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            List<PersonVO> persons = _personBusiness.FindByName(firstName, lastName);
            if (persons == null) return NotFound();
            return Ok(persons);
        }

        // GET api/paersons/5
        [HttpGet("{pageSize}/{page}")]
        [SwaggerResponse(200, Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<PersonVO> FindWithPagedSearch(int pageSize, int page)
        {
            List<PersonVO> persons = _personBusiness.FindWithPagedSearch(pageSize, page);
            if (persons == null) return NotFound();
            return Ok(persons);
        }

        // POST api/values
        [HttpPost]
        [SwaggerResponse(201, Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [SwaggerResponse(202, Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Update(person));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Delete(int id)
        {
            if(_personBusiness.Delete(id)) return NoContent();
            return NotFound();
        }
    }
}
