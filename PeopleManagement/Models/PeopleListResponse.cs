using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleManagement.Models
{
    /// <summary>
    /// People list response
    /// </summary>
    public class PeopleListResponse
    {
        public IEnumerable<PersonModel> People { get; set; }
        public int PageCount { get; set; }
    }
}