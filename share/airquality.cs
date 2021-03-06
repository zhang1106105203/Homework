using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public partial class airquality
    {
        public long Id { get; set; }
        [JsonProperty("SITENAME")]
        public string SiteName { get; set; }
        [JsonProperty("COUNTY")]
        public string County { get; set; }
        [JsonProperty("AQI")]
        public string AQI { get; set; }
        [JsonProperty("POLLUTANT")]
        public string Pollutant { get; set; }
        [JsonProperty("STATUS")]
        public string Status { get; set; }
        public object Image1 { get; set; }
        public object Image2 { get; set; }
    }
}
