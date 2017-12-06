using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace YakApi.Models
{
    [XmlRoot(ElementName = "herd")]
    public class Herd
    {
        [XmlElement(ElementName = "labyak")]
        public List<Labyak> Labyak { get; set; }
    }

}