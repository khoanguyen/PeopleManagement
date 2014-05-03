using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PeopleManagement.Models
{
    /// <summary>
    /// Model for adding a new version
    /// </summary>
    [DataContract]
    public class PersonAddModel
    {
        [DisplayName("FirstName")]
        [Required]
        [MaxLength(50)]
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }
        
        [DisplayName("LastName")]
        [Required]
        [MaxLength(50)]
        [DataMember(IsRequired = true)]
        public string LastName { get; set; }

        [DisplayName("Birthday")]
        [Required(ErrorMessage = "The Birthday field is required")]
        [DataMember(IsRequired = true)]        
        public System.DateTime Birthday { get; set; }

        [DisplayName("Email")]
        [Required]
        [DataMember(IsRequired = true)]
        [MaxLength(254)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        [DisplayName("Phone")]
        [Required]
        [DataMember(IsRequired = true)]
        [MaxLength(20)]
        [RegularExpression(@"^\d{7,20}$",
             ErrorMessage = "Please enter correct phone number, phone number contains 7-20 digits only")]
        public string Phone { get; set; }
    }
}