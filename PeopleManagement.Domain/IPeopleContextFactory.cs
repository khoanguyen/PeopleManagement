using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain
{
    /// <summary>
    /// PeopleDB context factory
    /// </summary>
    public interface IPeopleContextFactory
    {
        /// <summary>
        /// Creats a new database context
        /// </summary>
        /// <returns></returns>
        PeopleContext CreateContext();
    }
}
