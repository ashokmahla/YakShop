using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YakApi.Models
{
    public class Orders
    {
        public string Customer { get; set; }
        public ShopStock Order { get; set; }
    }
}