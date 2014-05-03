using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain.Model
{
    /// <summary>
    /// Implementation of Person Repository
    /// </summary>
    public class PeopleRepository : IRepository<Person>
    {
        public PeopleContext Context { get; private set; }

        public PeopleRepository(PeopleContext context)
        {
            Context = context;
        }

        public IQueryable<Person> List()
        {
            return Context.People;
        }

        public Person Get(params object[] keys)
        {
            return Context.People.Find(keys);
        }

        public void Add(Person entity)
        {
            Context.People.Add(entity);
        }

        public void Update(Person entity)
        {
            var entry = GetEntry(entity);
            entry.State = System.Data.EntityState.Modified;
        }

        public void Delete(Person entity)
        {
            var entry = GetEntry(entity);
            entry.State = System.Data.EntityState.Deleted;
        }

        private DbEntityEntry<Person> GetEntry(Person entity)
        {
            var entry = Context.Entry(entity);
            if (entry == null)
            {
                Context.People.Attach(entity);
                entry = Context.Entry(entity);
            }

            return entry;
        }
    }
}
