namespace WpfApp1.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class UserToken
    {
        public string Token { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
