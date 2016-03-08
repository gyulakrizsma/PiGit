using System.Runtime.Serialization;

namespace PiGit.Common
{
    [DataContract]
    public class GitMessage
    {
        [DataMember]
        public string ActionType { get; set; }
    }
}
