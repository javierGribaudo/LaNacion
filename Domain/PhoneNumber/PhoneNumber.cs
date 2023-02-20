
using System.Text.Json.Serialization;

namespace LaNacionChallenge.Domain
{
    public class PhoneNumber
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Number { get; set; }
        public int ContactId { get; set; }
        public bool Active { get; set; }
    }
}
