using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.viewmodel;

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
        [HttpGet]
        public IActionResult Index()
        {
            //var service = new ConsoleApp1.jsonservice();
            //var filePath = share.FilePath.GetFullPath("空氣品質.json");
            //List<airquality> data = service.LoadFormFile(filePath);
            //List<airquality> data = applicationDbContext.airqualities.ToList();
            //.Where(x => x.County.Contains("新北"))
            //.ToList();
            //return Json(data);
            return View();
        }
        [HttpPost]
        public IActionResult Search(AirqualityPearchParams searchParams)
        {
            /*var service = new ConsoleApp1.jsonservice();
            var filePath = share.FilePath.GetFullPath("空氣品質.json");
            var query = service.LoadFormFile(filePath).AsQueryable();*/
            var query = applicationDbContext.airqualities.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.Order))
            {
                query = query.Where(x => x.SiteName.Contains(searchParams.Keyword));
            }
            //.Where(x => x.SiteName.Contains(searchParams.Keyword));
            query = query.OrderByDescending(x => x.Id);
            //if (!string.IsNullOrEmpty(searchParams.Order))
            //{
            //    query = query.OrderBy(x => EF.Property<string>(x, searchParams.Order));
            //}
            query.Skip((searchParams.PageIndex) * 30).Take(30);

            return View("Index",query.ToList());
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ConsoleApp1.airquality create)
        {
            //var count = applicationDbContext.airqualities.Max(x => x.Id);

            //create.Id = count + 1;
            create.Id = 0;            
            
            applicationDbContext.airqualities.Add(create);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");            
        }
    }
}
