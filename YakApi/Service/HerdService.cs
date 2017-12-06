using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YakApi.Models;

namespace YakApi.Service
{
    public class HerdService : IHerdService
    {
        public HerdDataResponse GetHerdData(int elapsedTime)
        {
            
            var shopHerdList = new HerdDataResponse();
            shopHerdList.Herd = new List<ShopHerd>();
            try
            {
                var herdData = Utils.GetAllData();
                foreach (var herd in herdData.Labyak)
                {
                    var shopHerd = new ShopHerd()
                    {
                        Age = GetAge(herd.Age, elapsedTime),
                        Name = herd.Name,
                        AgeLastShaved = Convert.ToDecimal(herd.Age)
                    };
                    shopHerdList.Herd.Add(shopHerd);
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return shopHerdList;
        }
        private decimal GetAge(string age, int elapsedTime)
        {
            return ((Convert.ToDecimal(age) * 100) + elapsedTime) / 100;
        }
    }

}