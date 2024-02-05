using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogBl.DTOS;
using WerfLogDal.Models;

namespace WerfLogBl.Interfaces
{
   public interface IWerfManager
    {
        WerfDto AddWerf(WerfDto werfDto);
    }
}
