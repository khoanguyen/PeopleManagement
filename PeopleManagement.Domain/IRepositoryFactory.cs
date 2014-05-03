using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain
{
    /// <summary>
    /// Entity Repository Factory
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates a new Repository for Person entity
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        IRepository<Person> CreatePersonRepository(PeopleContext context);
    }
}
