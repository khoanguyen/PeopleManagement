using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain
{
    public class DefaultRepositoryFactory : IRepositoryFactory
    {
        public Model.IRepository<Model.Person> CreatePersonRepository(Model.PeopleContext context)
        {
            return new PeopleRepository(context);
        }
    }
}
