namespace efto_model.Models
{
    public class Quest_Requirement
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string Requirement {  get; set; }

        public Quest_Requirement(int questId, string requirement)
        {
            this.QuestId = questId;
            this.Requirement = requirement;
        }

        public Quest_Requirement(string requirement)
        {
            this.Requirement = requirement;
        }

        public Quest_Requirement() { }
    }
}
