namespace WerfLogDal.Models
{
    //[Table("Werven")]
    public  class Werf : BaseModel
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        //[MaxLength(150)]
        public string Naam { get; set; }

        //// Navigatie-eigenschappen
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Notitie> Notities { get; set; }

        ////[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Tijdregistratie> Tijdregistraties { get; set; }
    }
}
