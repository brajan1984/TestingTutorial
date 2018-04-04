using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {
        [HttpPost]
        public string GetMin([FromBody] string[] hours)
        {
            var result = "NOT POSSIBLE";

            try
            {
                if (hours.Count() > 0)
                {
                    var time = hours.Select(h => TimeSpan.Parse(h)).Min();

                    result = time.ToString();
                }
            }
            catch {}

            return result;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }
    }
}
