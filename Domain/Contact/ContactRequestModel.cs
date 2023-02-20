using System.Net;
using System.Text.Json.Serialization;
using LaNacionChallenge.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LaNacionChallenge.Domain
{
    public class ContactRequestModel
    {
      
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? ProfileImage { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("PhoneNumberIds")]
        public ICollection<int>? PhoneNumbers { get; set; }
        [JsonPropertyName("AddressId")]
        public int? Address { get; set; }
    }
}
