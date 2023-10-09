using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace Utiles
{
    public class JsonModel
    {
        [JsonInclude]
        public required MyProfile MyProfile { get; set; }
        [JsonInclude]
        public required ConnectionStrings ConnectionStrings { get; set; }
        [JsonInclude]
        public required Edcore Edcore { get; set; }
        [JsonInclude]
        public required OoManager OoManager { get; set; }
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
        public required string SeojungriOracle { get; set; }
        [JsonInclude]
        public required string SQLite { get; set; }
    }

    public class Edcore
    {
        [JsonInclude]
        public required Solace Solace { get; set; }
        [JsonInclude]
        public required SFTP SFTP { get; set; }
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
    public class SFTP
    {
        [JsonInclude]
        public required string host { get; set; }
        [JsonInclude]
        public required string port { get; set; }
        [JsonInclude]
        public required string username { get; set; }
        [JsonInclude]
        public required string password { get; set; }
    }

    public class OoManager
    {
        [JsonInclude]
        public required string FireBaseUrl { get; set; }
        [JsonInclude]
        public required string FireBaseAuth { get; set; }
    }
}
