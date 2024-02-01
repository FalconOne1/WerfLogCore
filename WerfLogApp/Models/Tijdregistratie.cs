using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogApp.Models
{
    public class Tijdregistratie
    {
        public string Id { get; set; }
        public string WerfId { get; set; } 
        public DateTime StartTijd { get; set; }
        public DateTime EindTijd { get; set; }
    }
}
