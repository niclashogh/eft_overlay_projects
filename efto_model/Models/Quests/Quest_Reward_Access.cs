namespace efto_model.Models.Quests
{
    public class Quest_Reward_Access
    {
        public int Id { get; set; }
        public string Access {  get; set; }

        public Quest_Reward_Access(string access) => this.Access = access;

        public Quest_Reward_Access() { }
    }
}
