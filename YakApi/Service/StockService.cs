using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using YakApi.Models;
using System.Web.Http;
using System.Net;

namespace YakApi.Service
{
    public class StockService : IStockService
    {
        private double ageLastShave = 0;
        private double age = 0; 
        public ShopStock GetStockData(int elapsedTime)
        {
            var totalMilk = 0.0M;
            var totalWool = 0;
            try
            {
                var herdData = Utils.GetAllData();

                foreach (var animal in herdData.Labyak)
                {
                    age = Convert.ToDouble(animal.Age);
                    ageLastShave = age * 100;
                    totalWool += GetSkin(elapsedTime);
                    totalMilk += GetTotalMilk(elapsedTime);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new ShopStock { Milk = totalMilk, Skins = totalWool };
        }

        public decimal GetTotalMilk(int elapsedTime)
        {
            var milkCount = 0M;
            var yakLife = Convert.ToInt32(Convert.ToDecimal(age) * 100);
            for (int i = 0; i < elapsedTime; i++)
            {
                milkCount += GetMilk(yakLife + i);
            }
            return milkCount;
        }
        private decimal GetMilk(int yaklife)
        {
            return (50 - (yaklife * 0.03M));
        }
        public int GetSkin(int elapsedTimeInDays)
        {
            int skinCount = 1;
            for (int day = 1; day < elapsedTimeInDays; day++)
            {
                int currentAgeInDays = (int)(age * 100 + day);
                if (CanShaveToday(currentAgeInDays))
                {
                    skinCount++;
                    ageLastShave = currentAgeInDays;
                }
            }
            return skinCount;
        }

        private bool CanShaveToday(int currentAgeInDays)
        {
            bool isEligibleForShave = false;
            double allowedGapInShave = (8 + ageLastShave * 0.01);
            if (IsAnimalAlive(currentAgeInDays))
            {
                isEligibleForShave = (currentAgeInDays - ageLastShave > allowedGapInShave) ? true : false;
            }

            return isEligibleForShave;
        }
        private bool IsAnimalAlive(int elapsedTimeInDays)
        {
            return (int)(age * 100 + elapsedTimeInDays) < 1000;
        }
      

    }
}