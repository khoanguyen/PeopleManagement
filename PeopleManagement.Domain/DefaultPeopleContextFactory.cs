using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain
{
    public class DefaultPeopleContextFactory : IPeopleContextFactory
    {
        public Model.PeopleContext CreateContext()
        {
            return new Model.PeopleContext();
        }
    }
}
