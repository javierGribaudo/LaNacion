using System.Net;
using System.Text.Json.Serialization;
using LaNacionChallenge.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LaNacionChallenge.Domain
{
    public class Contact
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? ProfileImage { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<PhoneNumber>? PhoneNumbers { get; set; }
        public Address? Address { get; set; }
        [JsonIgnore]
        public bool Active { get; set; }
    }
}
