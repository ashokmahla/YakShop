using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using YakApi.Models;
using YakApi.Service;

namespace YakApi.Controllers
{
    [RoutePrefix("api")]
    public class StockController : ApiController
    {
        IStockService _shopStockService;
        public StockController()
        {
            _shopStockService = new StockService();
        }

        [Route("yak-shop/stock/{id}")]
        public ShopStock Get(int id)
        {
           return _shopStockService.GetStockData(id);
        }

        [Route("yak-shop/order/{id}")]
        public HttpResponseMessage Post(Orders order,int id)
        {

            var stockData = _shopStockService.GetStockData(id);
            if (stockData.Milk >= order.Order.Milk && stockData.Skins >= order.Order.Skins)
            {
                return Request.CreateResponse(HttpStatusCode.Created, order.Order);
            }
            else if (stockData.Milk >= order.Order.Milk || stockData.Skins >= order.Order.Skins)
            {
                var output = stockData.Milk >= order.Order.Milk ? order.Order.Milk : order.Order.Skins;
                return Request.CreateResponse(HttpStatusCode.PartialContent, output);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }
    }
}
