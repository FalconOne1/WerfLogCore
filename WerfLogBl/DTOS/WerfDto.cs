using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogApp.Enums;

namespace WerfLogBl.DTOS
{
    public class WerfDto
    {
        public int? Id { get; set; }
        public string Naam { get; set; }

        //public List<NotitieDto> Notities { get; set; }

        //public List<TijdregistratieDto> Tijdregistraties { get; set; }
    }


}
