using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "Jotain", "Lisää", "Arvo", "Toinen arvo" };
        }

        // GET api/values/Jussi
        [HttpGet("{nimi}")]
        public ActionResult<string> Get(string nimi)
        {
            return "Moikka " + nimi + "!";
        }

        // GET api/values/Jussi/Makkonen
        [HttpGet("{etunimi}/{sukunimi}")]
        public ActionResult<string> Get(string etunimi, string sukunimi)
        {
            return "Päivää " + etunimi + " " + sukunimi + "!";
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]

    public class DocumentationController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("")]
        public string Document()
        {
            return "Documentation available";
        }

        // GET api/Documentation/"keycode"
        [HttpGet]
        [Route("{key}")]

        public ActionResult GetDoc(string key)
        {
            NorthwindContext context = new NorthwindContext();

            List<Documentation> privateDocList = (from d in context.Documentation
                                                  where d.Keycode == key
                                                  select d).ToList();
            if (privateDocList.Count > 0)
            {
                return Ok(privateDocList);
            }
            else
            {
                return BadRequest("Koodilla jonka annoit ei ole dokumentaatiota, tämä päivä ja aika: " + DateTime.Now.ToString());
            }
        }
    }
}
