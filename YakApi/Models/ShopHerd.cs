using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YakApi.Models
{
    
    public class ShopHerd
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public decimal Age { get; set; }
        [JsonProperty("age-last-shaved")]
        public decimal AgeLastShaved { get; set; }
    }

    public class HerdDataResponse
    {
        [JsonProperty("herd")]
        public List<ShopHerd> Herd { get; set; }
    }
}