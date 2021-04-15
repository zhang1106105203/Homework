using ConsoleApp10318;
using Newtonsoft.Json.Linq;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlService1 = new xmlservice();

            var datas = xmlService1.LoadFormFile("懸浮粒子.xml");


            Console.WriteLine(string.Format("分析完成，共有{0}筆資料", datas.Count));
            datas.ForEach(x =>
            {
                Console.WriteLine(string.Format("縣市 :{0} 單位:{1} 地區:{2} PM2.5值:{3} 日期:{4}", x.county, x.itemunit, x.site, x.pm25, x.datacreationdate));
            });


            var jsonService = new jsonservice();


            var jsonDatas = jsonService.LoadFormFile("空氣品質.json");

            Console.WriteLine(string.Format("分析完成，共有{0}筆資料", jsonDatas.Count));
            jsonDatas.ForEach(x =>
            {
                Console.WriteLine(string.Format("地區 :{0} 縣市:{1} 空氣品質:{2} 品質程度:{3}", x.SiteName, x.County, x.AQI,x.Status));
            });

            Console.ReadKey();
        }
    }
}