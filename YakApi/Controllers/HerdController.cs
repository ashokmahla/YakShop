using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using YakApi.Models;
using YakApi.Service;
using Newtonsoft.Json;



namespace YakApi.Controllers
{
    [RoutePrefix("api")]
    public class HerdController : ApiController
    {
        IHerdService _shopHerdService;

        public HerdController()
        {
            _shopHerdService = new HerdService();
        }
        [Route("yak-shop/herd/{id}")]
        public HerdDataResponse Get(int id)
        {
           return _shopHerdService.GetHerdData(id);
           
        }
    }
}