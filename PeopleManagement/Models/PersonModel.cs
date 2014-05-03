using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PeopleManagement.Models
{
    /// <summary>
    /// Person query model
    /// </summary>
    [DataContract]
    public class PersonModel
    {
        [DataMember]
        public int PersonId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Birthday { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public Nullable<System.DateTime> LastModified { get; set; }

        public static PersonModel FromPerson(Person person)
        {
            return new PersonModel
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birthday = person.Birthday.ToString("MM/dd/yyyy"),
                Email = person.Email,
                Phone = person.Phone,
                LastModified = person.LastModified
            };
        }

    }
}