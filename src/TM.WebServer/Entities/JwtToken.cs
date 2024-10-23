namespace TM.WebServer.Entities
{
    public class JwtToken
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}
