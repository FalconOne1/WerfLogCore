using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogBl.DTOS

{
    public class NotitieDto
    {
        public int? Id { get; set; }
        public int WerfId { get; set; }
        public string Tekst { get; set; }
        public DateTime Datum { get; set; }

        //public WerfDto Werf { get; set; }
    }
}
