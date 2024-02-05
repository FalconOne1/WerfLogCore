using SQLite;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogDal.Models
{
    public  class Tijdregistratie : BaseModel
    {
    //    [PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        public DateTime StartTijd { get; set; }
        public DateTime StopTijd { get; set; }

        // Expliciet definiëren van de foreign key-naam
        //[ForeignKey(typeof(Werf))]
        public int WerfId { get; set; }

        // Navigatie-eigenschappen
        //[ManyToOne]
        //public Werf Werf { get; set; }
    }
}
