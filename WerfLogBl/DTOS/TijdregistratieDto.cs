using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogBl.DTOS
{
    public class TijdregistratieDto
    {
        public int? Id { get; set; }
        public int WerfId { get; set; }
        public DateTime StartTijd { get; set; }
        public DateTime? EindTijd { get; set; }

        public int TotaleTijd { get; set; }
        public string WerfNaamRegistratie { get; set; }
    }
}
