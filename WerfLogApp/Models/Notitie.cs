using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogApp.Models
{
    public class Notitie
    {
        public string Id { get; set; }
        public string WerfId { get; set; } 
        public string Tekst { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
    }
}
