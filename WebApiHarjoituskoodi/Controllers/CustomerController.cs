using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private NorthwindContext db = new NorthwindContext();

        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers() //Hakee kaikki rivit
        {
            NorthwindContext db = new NorthwindContext();
            List<Customers> asiakkaat = db.Customers.ToList();
            return asiakkaat;
        }

        [HttpGet]
        [Route("{id}")]
        public Customers GetOneCustomer(string id) //Find-metodi hakee AINA VAIN PÄÄAVAIMELLA YHDEN RIVIN
        {
            NorthwindContext db = new NorthwindContext();
            Customers asiakas = db.Customers.Find(id);
            return asiakas;
        }

        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetSomeCustomers(string key) //Hakee jollain tiedolla mätsäävät rivit
        {
            NorthwindContext db = new NorthwindContext();

            var someCustomers = from c in db.Customers
                                where c.Country == key
                                select c;
            
            return someCustomers.ToList();
        }

        [HttpPost] // <-- filtteri, joka sallii vain POST-metodit (Http-verbit)
        [Route("")] // <-- Routen placeholder

        public ActionResult PostCreateNew([FromBody] Customers asiakas)
        {
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                return Ok(asiakas.CustomerId); //Palauttaa vastaluodun uuden asikkaan avaimen arvon
            }
            catch (Exception e)
            {
                return BadRequest("Asiakkaan lisääminen ei onnistunut. Alla lisätietoa" + e);
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpDelete]
        [Route("{key}")]

        public ActionResult DeleteCustomer(string key)
        {
            try
            {
                Customers asiakas = db.Customers.Find(key);
                if (asiakas != null)
                {
                    try
                    {
                        db.Customers.Remove(asiakas);
                        db.SaveChanges();
                        Console.WriteLine(key + " poistettiin");
                        return Ok("Asiakas " + key + " poistettiin");
                    }
                    catch (Exception e)
                    {
                        return BadRequest("Poistaminen ei onnistunut" + e);
                    }
                }
                else
                {
                    return NotFound("Asiakas" + key + " ei löydy");
                }
                }
                finally
            {
                db.Dispose();
            }          
        }

        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(string key, [FromBody] Customers asiakas)
        {
            try
            {
                Customers customer = db.Customers.Find(key);
                if(customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
					customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;
                    customer.Fax = asiakas.Fax;

                    db.SaveChanges();
                    return Ok(customer.CustomerId);
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen päivitettäessä" + e);
            }
            finally
            {
                db.Dispose();
            }
        }

    }
}
