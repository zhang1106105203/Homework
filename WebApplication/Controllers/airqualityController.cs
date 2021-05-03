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
            return Search(new AirqualityPearchParams());
        }
        [HttpPost]
        public IActionResult Search(AirqualityPearchParams searchParams)
        {            
            var query = applicationDbContext.airqualities.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.Order))
            {
                query = query.Where(x => x.SiteName.Contains(searchParams.Keyword));
            }
            query = query.OrderByDescending(x => x.Id);
            query.Skip((searchParams.PageIndex) * 30).Take(30);
            ViewBag.SearchParams = searchParams;

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
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = applicationDbContext.airqualities.Find(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ConsoleApp1.airquality edit)
        {
            applicationDbContext.Entry(edit).State = EntityState.Modified;                     
            applicationDbContext.Update(edit);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(long id)
        {
            var model = applicationDbContext.airqualities.Find(id);
            applicationDbContext.airqualities.Remove(model);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
