using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp;

namespace ConsoleApp10318
{
    public class xmlservice
    {
        public xmlservice () {}
        public List<data> LoadFormFile(string filePath)
        {
            var str = System.IO.File.ReadAllText(filePath);
            var xDoucument = System.Xml.Linq.XDocument.Parse(str);
            var targets = xDoucument.Descendants("data");

            return targets
                .Select(x =>
                {
                    var item = new data();
                    item.county = x.Element("county").Value;
                    item.itemunit = x.Element("itemunit").Value;
                    item.site = x.Element("site").Value;
                    item.pm25 = x.Element("pm25").Value;
                    item.datacreationdate = x.Element("datacreationdate").Value;

                    return item;
                })
                .ToList();
        }
    }
}
