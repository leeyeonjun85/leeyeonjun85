using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Wpf_DataBase.Models
{
    [DataContract]
    public class JsonModel
    {
        [DataMember]
        public MyProfile? MyProfile { get; set; }

        [DataMember]
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    [DataContract]
    public class MyProfile
    {
        [DataMember]
        public string? Name { get; set; }

        [DataMember]
        public string? Company { get; set; }

        [DataMember]
        public string? BirthDay { get; set; }

        [DataMember]
        public string? Email { get; set; }
    }

    [DataContract]
    public class ConnectionStrings
    {
        [DataMember]
        public string? DefaultConnection { get; set; }
    }
}
