using System;
using System.Xml;

namespace XMLReadAndParse
{
    class XMLReadandParse
    {
        static void Main(string[] args)
        {
            using (XmlReader reader = XmlReader.Create(@"https://data.epa.gov.tw/api/v1/aqx_p_02?limit=1000&api_key=9be7b239-557b-4c10-9775-78cadfc555e9&format=xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "county":
                                Console.WriteLine("城市:" + reader.ReadString());                                
                                break;
                            case "Site":
                                Console.WriteLine("地區:" + reader.ReadString());
                                break;
                            case "PM25":
                                Console.WriteLine("PM2.5值:" + reader.ReadString());
                                break;
                            case "ItemUnit":
                                Console.WriteLine("單位:" + reader.ReadString());
                                break;
                            case "DataCreationDate":
                                Console.WriteLine("日期:" + reader.ReadString());
                                break;
                        }
                    }
                    Console.WriteLine("");
                }
                Console.ReadKey();
            }
        }
    }
}