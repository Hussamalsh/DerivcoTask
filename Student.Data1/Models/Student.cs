using ProtoBuf;
using System;
using System.Runtime.Serialization;

namespace Student.Data.Models
{
    [Serializable]
    [ProtoContract]
    public class Student
    {
        [DataMember]
        [ProtoMember(1)]
        public Guid ID { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string FirstName { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public string LastName { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string Email { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public string Address { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public string City { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public string ZIP { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public string Phone { get; set; }
    }
}
