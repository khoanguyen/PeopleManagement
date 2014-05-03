using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PeopleManagement.Models
{
    /// <summary>
    /// Model for removing Person
    /// </summary>
    [DataContract]
    public class PersonRemoveModel
    {
        [DataMember]
        public int PersonId { get; set; }

        [DataMember]
        public DateTime? LastModified { get; set; }
    }
}