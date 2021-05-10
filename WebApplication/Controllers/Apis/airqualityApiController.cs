using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Data;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApplication.Controllers.Apis
{
    [Route("api/airquality")]
    [ApiController]
    public class airqualityApiController:ControllerBase
    {
        public ApplicationDbContext applicationDbContext;
        public airqualityApiController(
            ApplicationDbContext applicationDbContext
            )
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<List<airquality>> GetAsync() 
        {
            var data = await this.applicationDbContext.airqualities.ToListAsync();
            return data;
        }
        [HttpGet]
        public async Task<airquality> GetAsync(string id)
        {
            var data = await this.applicationDbContext.airqualities.FindAsync(id);
            return data;
        }
        [HttpPost]
        public async Task<airquality> Post(airquality input)
        {
            applicationDbContext.airqualities.Add(input);
            await applicationDbContext.SaveChangesAsync();
            return input;
        }
        [HttpPut]
        public async Task<airquality> Put(airquality input)
        {
            applicationDbContext.airqualities.Add(input);
            await applicationDbContext.SaveChangesAsync();
            return input;
        }
        [HttpDelete]
        public async Task<airquality> Delete(airquality input)
        {
            //applicationDbContext.airqualities.Add(input);
            //await applicationDbContext.SaveChangesAsync();
            //return input;
        }
    }
}
