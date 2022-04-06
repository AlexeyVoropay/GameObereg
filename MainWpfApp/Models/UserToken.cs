namespace WpfApp1.Domain
{
    using System;
    using System.Text.Json.Serialization;

    public class UserToken
    {
        public string Token { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
