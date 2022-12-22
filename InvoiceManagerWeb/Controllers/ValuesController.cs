using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace InvoiceManagerWeb.Controllers
{
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private ILogger _log;
        public ValuesController(IHostingEnvironment hostingEnvironment, ILogger<ValuesController> log)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._log = log;
        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            var path = Content(webRootPath + "\n" + contentRootPath);
            
            var values = Environment.GetEnvironmentVariables();
            this._log.LogInformation("This is test message");
            
            return Ok(new {webrootpath = webRootPath, contentRootPath = contentRootPath });
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
