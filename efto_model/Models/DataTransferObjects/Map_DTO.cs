using efto_model.Models.Enums;

namespace efto_model.Models
{
    public class Map_DTO
    {
        public Maps Map { get; set; }
        public string ActiveQuestFeedback { get; set; }

        public Map_DTO(Maps map)
        {
            this.Map = map;
        }
    }
}
