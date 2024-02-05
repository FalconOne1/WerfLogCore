using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogBl.DTOS
{
    internal class ProjectOverlegDto
    {
        public int? Id { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
    }
}
