namespace efto_model.Models
{
    public class Extraction_Requirement
    {
        public int Id { get; set; }
        public int ExtractionId { get; set; }
        public string Requirement { get; set; }

        public Extraction_Requirement(int extractionId, string requirement)
        {
            this.ExtractionId = extractionId;
            this.Requirement = requirement;
        }

        public Extraction_Requirement() { }
    }
}
