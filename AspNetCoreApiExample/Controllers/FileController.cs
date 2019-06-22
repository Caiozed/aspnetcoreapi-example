using AspNetCoreApiExample.Business;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IO;

namespace AspNetCoreApiExample.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        // GET api/values
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(byte[]))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [SwaggerResponse(404)]
        [Authorize("Bearer")]
        public ActionResult Get()
        {
            var stream = new FileStream(_fileBusiness.GetFile(), FileMode.Open);
            return File(stream, "application/pdf", $"{DateTime.Now}.pdf");
        }

    }
}