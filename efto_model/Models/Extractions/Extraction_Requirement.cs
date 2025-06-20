namespace efto_model.Models.Extractions
{
    public class Extraction_Requirement
    {
        public int Id { get; set; }
        public int ExtractionId { get; set; }
        public string Requirement { get; set; }

        public Extraction_Requirement(int extractionId, string requirement)
        {
            ExtractionId = extractionId;
            Requirement = requirement;
        }

        public Extraction_Requirement() { }
    }
}
