using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace LaNacionChallenge.Domain
{
    public class Address
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public int ContactId { get; set; }
        public bool Active { get; set; }
    }
}
