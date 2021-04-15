using ConsoleApp10318;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp10318
{
    public class jsonservice
    {
        public List<airquality> LoadFormFile(string filePath)
        {
            var str = System.IO.File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<airquality>>(str);
        }
    }
}
