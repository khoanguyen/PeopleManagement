using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain.Services
{
    /// <summary>
    /// Interface of Person Management logic
    /// </summary>
    public interface IPeopleManagementService
    {
        /// <summary>
        /// Gets the list of people
        /// </summary>
        /// <param name="page">Page index</param>
        /// <param name="size">Page size</param>
        /// <returns></returns>
        IEnumerable<Person> List(int page, int size);

        /// <summary>
        /// Counts the total pages
        /// </summary>
        /// <param name="size">Page size</param>
        /// <returns></returns>
        int CountPage(int size);

        /// <summary>
        /// Finds person with given ID
        /// </summary>
        /// <param name="personId">Person's ID</param>
        /// <returns></returns>
        Person FindPerson(int personId);

        /// <summary>
        /// Adds a new person
        /// </summary>
        /// <param name="firstName">Person's FirstName</param>
        /// <param name="lastName">Person's LastName</param>
        /// <param name="birthday">Person's Birthday</param>
        /// <param name="email">Person's Email</param>
        /// <param name="phone">Person's Phone</param>
        Person AddPerson(string firstName, string lastName, DateTime birthday, string email, string phone);

        /// <summary>
        /// Updates information of Person with given ID 
        /// </summary>
        /// <param name="personId">Person's ID</param>
        /// <param name="firstName">Person's FirstName</param>
        /// <param name="lastName">Person's LastName</param>
        /// <param name="birthday">Person's Birthday</param>
        /// <param name="email">Person's Email</param>
        /// <param name="phone">Person's Phone</param>
        /// <param name="lastModified">LastModified timestamp of data</param>
        Person UpdatePerson(int personId, string firstName, string lastName, DateTime birthday, string email, string phone, DateTime? lastModified);

        /// <summary>
        /// Deletes Person with given ID
        /// </summary>
        /// <param name="personId">Person's ID</param>
        /// <param name="lastModified">LastModified timestamp of data</param>
        void DeletePerson(int personId, DateTime? lastModified);
    }
}
