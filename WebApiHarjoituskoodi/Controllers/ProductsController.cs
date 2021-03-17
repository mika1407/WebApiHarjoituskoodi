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
    public class ProductsController : Controller
    {
        private NorthwindContext db = new NorthwindContext();

        [HttpGet]
        [Route("{id}")]
        public Products GetOneProduct(int id) // Haku pääavaimella/yksi rivi , Find-metodi hakee AINA VAIN PÄÄAVAIMELLA YHDEN RIVIN
        {
            NorthwindContext db = new NorthwindContext();
            Products tuote = db.Products.Find(id);
            return tuote;
        }

        [HttpGet]
        [Route("")]
        public List<Products> GetAllProducts() //Hakee kaikki rivit
        {
            NorthwindContext db = new NorthwindContext();
            List<Products> tuotteet = db.Products.ToList();
            return tuotteet;
        }


        [HttpGet]
        [Route("supplierID/{key}")]
        public List<Products> GetSomeProducts(int key) //Hakee jollain tiedolla mätsäävät rivit
        {
            NorthwindContext db = new NorthwindContext();

            var someProducts = from c in db.Products
                                where c.SupplierId == key
                                select c;

            return someProducts.ToList();
        }

        [HttpPost]      //IDENTITY_INSERT is set to OFF, joten "productId": "" :tä ei syötetä !!!
        [Route("")]

        public ActionResult PostCreateNew([FromBody] Products tuote)
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                db.Products.Add(tuote);
                db.SaveChanges();
                return Ok(tuote.ProductId);
            }
            catch (Exception e)
            {
                return BadRequest("Tuotteet lisääminen ei onnistunut. Alla lisätietoa" + e);
            }
            finally
            {
                db.Dispose();
            }
        }


        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(int key, [FromBody] Products tuote)
        {
            try
            {
                Products product = db.Products.Find(key);
                if (product != null)
                {
                    product.ProductName = tuote.ProductName;
                    product.SupplierId = tuote.SupplierId;
                    product.CategoryId = tuote.CategoryId;
                    product.QuantityPerUnit = tuote.QuantityPerUnit;
                    product.UnitPrice = tuote.UnitPrice;
                    product.UnitsInStock = tuote.UnitsInStock;
                    product.UnitsOnOrder = tuote.UnitsOnOrder;
                    product.ReorderLevel = tuote.ReorderLevel;
                    product.Discontinued = tuote.Discontinued;
                    product.Category = tuote.Category;
                    product.Supplier = tuote.Supplier;
                    product.OrderDetails = tuote.OrderDetails;

                    db.SaveChanges();
                    return Ok(product.ProductId);
                }
                else
                {
                    return NotFound("Päivitettävää asikasta ei löytynyt");
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

        [HttpDelete]        //https://forums.asp.net/t/1389200.aspx?error+deleteing+from+Northwind
        [Route("{key}")]    //https://www.techjunkieblog.com/2014/07/the-delete-statement-conflicted-with.html

        public ActionResult DeleteProduct(int key)
        {
            try
            {
                Products tuote = db.Products.Find(key);
                if (tuote != null)
                {
                    try
                    {
                        db.Products.Remove(tuote);
                        db.SaveChanges();
                        Console.WriteLine(key + " poistettiin");
                        return Ok("Tuote " + key + " poistettiin");
                    }
                    catch (Exception e)
                    {
                        return BadRequest("Poistaminen ei onnistunut" + e);
                    }
                }
                else
                {
                    return NotFound("Tuote" + key + " ei löydy");
                }
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
