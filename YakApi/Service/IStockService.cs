using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YakApi.Models;

namespace YakApi.Service
{
   public interface IStockService
    {
        ShopStock GetStockData(int elapsedTime);
    }
}
