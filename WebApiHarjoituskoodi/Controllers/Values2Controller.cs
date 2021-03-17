using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Values2Controller : ControllerBase
    {
        // GET api/valuesbfloth
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5qpqijfpijf
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }




    }
}
