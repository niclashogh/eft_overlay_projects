namespace efto_model.Models.Extractions
{
    public class Extraction_Type
    {
        public int Id { get; set; }
        public string Desc { get; set; }

        public Extraction_Type(string desc) => this.Desc = desc;

        public Extraction_Type() { }
    }
}
