using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private NorthwindContext db = new NorthwindContext();

        [HttpGet]
        [Route("{id}")]
        public Employees GetOneEmployee(int id) // Haku pääavaimella/yksi rivi 
        {
            NorthwindContext db = new NorthwindContext();
            Employees alainen = db.Employees.Find(id);
            return alainen;
        }

        [HttpGet]
        [Route("")]
        public List<Employees> GetAllEmployees() //Hakee kaikki rivit
        {
            NorthwindContext db = new NorthwindContext();
            List<Employees> alaiset = db.Employees.ToList();
            return alaiset;
        }

        [HttpGet]
        [Route("City/{key}")]
        public List<Employees> GetSomeEmployees(string key) //Hakee jollain tiedolla eli kaupungin nimellä rivit
        {
            NorthwindContext db = new NorthwindContext();

            var someEmployees = from c in db.Employees
                               where c.City == key
                               select c;

            return someEmployees.ToList();
        }

        [HttpPost]      // POST lisäytä tehdessä pitää ensimmäinen arvo: employeeId jättää pois Postmanissa !
        [Route("")]

        public ActionResult PostCreateNew([FromBody] Employees alainen)
        {
            //NorthwindContext db = new NorthwindContext();
            try
            {
                db.Employees.Add(alainen);
                db.SaveChanges();
                return Ok(alainen.EmployeeId);
            }
            catch (Exception e)
            {
                return BadRequest("Työntekijän lisääminen ei onnistunut. Alla lisätietoa" + e);
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(int key, [FromBody] Employees alainen)
        {
            try
            {
                Employees employee = db.Employees.Find(key);
                if (employee != null)
                {
                    employee.EmployeeId = alainen.EmployeeId;
                    employee.LastName = alainen.LastName;
                    employee.FirstName = alainen.FirstName;
                    employee.Title = alainen.Title;
                    employee.TitleOfCourtesy = alainen.TitleOfCourtesy;
                    employee.BirthDate = alainen.BirthDate;
                    employee.HireDate = alainen.HireDate;
                    employee.Address = alainen.Address;
                    employee.City = alainen.City;
                    employee.Region = alainen.Region;
                    employee.PostalCode = alainen.PostalCode;
                    employee.Country = alainen.Country;
                    employee.HomePhone = alainen.HomePhone;
                    employee.Extension = alainen.Extension;
                    employee.Photo = alainen.Photo;
                    employee.Notes = alainen.Notes;
                    employee.ReportsTo = alainen.ReportsTo;
                    employee.PhotoPath = alainen.PhotoPath;
                    employee.ReportsToNavigation = alainen.ReportsToNavigation;
                    employee.EmployeeTerritories = alainen.EmployeeTerritories;
                    employee.InverseReportsToNavigation = alainen.InverseReportsToNavigation;
                    employee.Orders = alainen.Orders;

                    db.SaveChanges();
                    return Ok(employee.EmployeeId);
                }
                else
                {
                    return NotFound("Päivitettävää työntekijää ei löytynyt");
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

        [HttpDelete]        
        [Route("{key}")]    

        public ActionResult DeleteEmployee(int key)
        {
            try
            {
                Employees alainen = db.Employees.Find(key);
                if (alainen != null)
                {
                    try
                    {
                        db.Employees.Remove(alainen);
                        db.SaveChanges();
                        Console.WriteLine(key + " poistettiin");
                        return Ok("Työntekijä " + key + " poistettiin");
                    }
                    catch (Exception e)
                    {
                        return BadRequest("Poistaminen ei onnistunut" + e);
                    }
                }
                else
                {
                    return NotFound("Työntekijää " + key + " ei löydy");
                }
            }
            finally
            {
                db.Dispose();
            }
        }

    }
}
