﻿using System.Text.Json.Serialization;

namespace Incidents.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
    }
}
