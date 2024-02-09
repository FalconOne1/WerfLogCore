using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogBl.DTOS;

namespace WerfLogBl.Interfaces
{
    public  interface ITijdregistratieManager
    {
        Task<int> VoegStarttijdWerfToe(WerfDto werf);
        Task VoegStoptijdWerfToe(int actieveTijdId);

        Task<TijdregistratieDto> GetActieveTijdregistratieId();
    }
}
