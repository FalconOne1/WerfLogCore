using SQLite;

namespace WerfLogDal.Models
{

    //[Table("Notities")]
    public  class Notitie : BaseModel
    {

        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        public string Tekst { get; set; }
        public DateTime Datum { get; set; }


        // Expliciet definiëren van de foreign key-naam
        //[ForeignKey(typeof(Werf))]
        public int WerfId { get; set; } 

        //Navigatie-eigenschappen
       //[ManyToOne]
        //public Werf Werf { get; set; }
    }
}
