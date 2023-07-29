﻿using System.Text.Json.Serialization;

namespace CakeCapitalCheckout.Models
{
    public class AirwallexToken
    {
        [JsonPropertyName("expires_at")]
        public string ExpiresAt { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
