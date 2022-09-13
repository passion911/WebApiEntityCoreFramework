using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using StringLibrary;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_EndPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentController>
        [HttpGet(Name ="Get_Employees")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [SwaggerOperation(OperationId = "Get All Employees")]
        public ActionResult<IEnumerable<string>> GetEmployees()
        {
            var verify = StringUtilities.StartsWithUpper("Hello");
            var size = HttpContext.Features.Get<IHttpMaxRequestBodySizeFeature>()?.MaxRequestBodySize;
            if ((new Random()).Next() % 2 == 0)
            {
                return Ok(new string[] { "value1", "value2" });
            }
            else
            {
                return Problem(detail: "No Items Found, Don't Try Again!");
            }

        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
