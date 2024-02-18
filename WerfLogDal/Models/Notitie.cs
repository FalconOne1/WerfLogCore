namespace WerfLogDal.Models
{

    //[Table("Notities")]
    public class Notitie : BaseModel

    /* Unmerged change from project 'WerfLogDal (net8.0-android)'
    Before:
        {


            //[PrimaryKey, AutoIncrement]
    After:
        {


            //[PrimaryKey, AutoIncrement]
    */

    /* Unmerged change from project 'WerfLogDal (net8.0-maccatalyst)'
    Before:
        {


            //[PrimaryKey, AutoIncrement]
    After:
        {


            //[PrimaryKey, AutoIncrement]
    */

    /* Unmerged change from project 'WerfLogDal (net8.0-windows10.0.19041.0)'
    Before:
        {


            //[PrimaryKey, AutoIncrement]
    After:
        {


            //[PrimaryKey, AutoIncrement]
    */
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
