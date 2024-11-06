using System.Text.Json.Serialization;

namespace Incidents.Domain.Entities
{
    public class Incident
    {
        public int Id { get; set; }
        public string IncidentName { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
    }
}
