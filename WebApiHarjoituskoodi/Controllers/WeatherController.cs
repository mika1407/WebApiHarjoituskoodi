using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : Controller
    {
        [HttpGet] 
        public string GetPorvoo() 
        { 
            WebClient client = new WebClient(); 
            try 
        { 
            string data = client.DownloadString("https://ilmatieteenlaitos.fi/saa/porvoo"); 
                int index = data.IndexOf("temperature"); 
                if (index > 0) 
                { 
                    string weather = data.Substring(index + 34, 4); 
                    return weather;
                } 
            } 
            finally
            { 
                client.Dispose(); 
            } 
            return "(unknown)"; 
        }

        [HttpGet] 
        [Route("{key}")] 
        public string GetWeather(string key) 
        { 
            WebClient client = new WebClient(); 
            try 
        { 
            string data = client.DownloadString("https://ilmatieteenlaitos.fi/saa/" + key); 
                int index = data.IndexOf("temperature"); 
                if (index > 0) 
                { 
                    string weather = data.Substring(index + 34, 4).Replace("<", "").Replace("/", ""); 
                    return weather;
                } 
            } 
            finally 
            { 
                client.Dispose(); 
            } 
            return "(unknown)"; 
        }
    }
}
