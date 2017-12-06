using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace YakApi.Models
{
    [XmlRoot(ElementName = "labyak")]
    public class Labyak
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "age")]
        public string Age { get; set; }
        [XmlAttribute(AttributeName = "sex")]
        public string Sex { get; set; }
    }
}