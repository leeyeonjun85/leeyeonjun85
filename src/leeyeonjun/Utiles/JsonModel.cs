using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace leeyeonjun.Utiles
{
    public class JsonModel
    {
        [JsonInclude]
        public required MyProfile MyProfile { get; set; }
        [JsonInclude]
        public required ConnectionStrings ConnectionStrings { get; set; }
        [JsonInclude]
        public required Edcore Edcore { get; set; }
    }

    public class MyProfile
    {
        [JsonInclude]
        public required string Name { get; set; }
        [JsonInclude]
        public required string Company { get; set; }
        [JsonInclude]
        public required string BirthDay { get; set; }
        [JsonInclude]
        public required string Email { get; set; }
    }

    public class ConnectionStrings
    {
        [JsonInclude]
        public required string DefaultConnection { get; set; }
    }

    public class Edcore
    {
        [JsonInclude]
        public required Solace Solace { get; set; }
    }

    public class Solace
    {
        [JsonInclude]
        public required string HostIP { get; set; }
        [JsonInclude]
        public required string VPN { get; set; }
        [JsonInclude]
        public required string UserName { get; set; }
        [JsonInclude]
        public required string Password { get; set; }
    }
}
