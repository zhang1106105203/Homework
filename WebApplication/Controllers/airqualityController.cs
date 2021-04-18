using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    public class airqualityController : Controller
    {
        public ApplicationDbContext applicationDbContext;
        public airqualityController(
            ApplicationDbContext applicationDbContext
            )
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var service = new ConsoleApp1.jsonservice();
            var filePath = share.FilePath.GetFullPath("空氣品質.json");
            List<airquality> data = service.LoadFormFile(filePath)
            //List<airquality> data = applicationDbContext.airqualities.ToList();
            .Where(x => x.County.Contains("新北"))
            .ToList();
            //return Json(data);
            return View(data);
        }

        [HttpGet]
        public IActionResult Import()
        {
            var airqualityservice = new ConsoleApp1.jsonservice();
            List<airquality> airqualitydata = airqualityservice.LoadFormFile(share.FilePath.GetFullPath("空氣品質.json"));

            var PM_25service = new ConsoleApp1.xmlservice();
            List<data> PM_25data = PM_25service.LoadFormFile(share.FilePath.GetFullPath("懸浮粒子.xml"));

            //airqualitydata.ForEach(x =>
            //{
            //    applicationDbContext.airqualities.Add(x);
            //});

            applicationDbContext.airqualities.AddRange(airqualitydata);
            applicationDbContext.datas.AddRange(PM_25data);

            applicationDbContext.SaveChanges();

            return Content("OK");
        }
    }
}
