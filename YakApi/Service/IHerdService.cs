using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YakApi.Models;

namespace YakApi.Service
{
   public interface IHerdService
    {
        HerdDataResponse GetHerdData(int elapsedTime);
    }
}
