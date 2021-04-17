using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class airqualityController : Controller
    {
        public IActionResult Index()
        {
            var service = new ConsoleApp1.jsonservice();
            var filePath = share.FilePath.GetFullPath("空氣品質.json");
            List<airquality> data = service.LoadFormFile(filePath);
            //return Json(data);
            return View(data);
        }
    }
}
