using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using YakApi.Models;

namespace YakApi
{
    public class Utils
    {
        public static Herd GetAllData()
        {
           var filePath = ConfigurationManager.AppSettings["YakFilePath"].ToString();
           return ParseXmlToObject(filePath);
        }
        private static Herd ParseXmlToObject(string filePath)
        {
            var herdObject = new Herd();
            string xmlString = System.IO.File.ReadAllText(filePath);
            using (TextReader reader = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Herd));
                herdObject = (Herd)serializer.Deserialize(reader);
            }
            return herdObject;
        }
    }
}