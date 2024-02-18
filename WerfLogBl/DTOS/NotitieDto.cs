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
