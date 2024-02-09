using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerfLogDal.Models
{
    public  class ProjectOverleg : BaseModel
    {
      

        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
    }
}
