using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain.Services
{
    /// <summary>
    /// Implementation of Person Management logic
    /// </summary>
    public class PeopleManagementService : IPeopleManagementService
    {
        public IPeopleContextFactory ContextFactory { get; private set; }
        public IRepositoryFactory RepositoryFactory { get; private set; }

        public PeopleManagementService(IPeopleContextFactory contextFactory, IRepositoryFactory repositoryFactory)
        {
            ContextFactory = contextFactory;
            RepositoryFactory = repositoryFactory;
        }

        public IEnumerable<Model.Person> List(int page, int size)
        {
            if (size <= 0)
                throw new ArgumentException("Page size");

            using (var context = ContextFactory.CreateContext())
            {
                var repository = RepositoryFactory.CreatePersonRepository(context);
                return repository.List()
                                 .OrderBy(p => p.FirstName)
                                 .Skip(size * page)
                                 .Take(size)
                                 .ToList();
            }
        }

        public int CountPage(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Page size");

            using (var context = ContextFactory.CreateContext())
            {
                var repository = RepositoryFactory.CreatePersonRepository(context);
                var total = repository.List().Count();
                return (total / size) + (total % size > 0 ? 1 : 0);
            }
        }

        public Model.Person FindPerson(int personId)
        {
            using (var context = ContextFactory.CreateContext())
            {
                var repository = RepositoryFactory.CreatePersonRepository(context);
                return repository.Get(personId);
            }
        }

        public Person AddPerson(string firstName, string lastName, DateTime birthday, string email, string phone)
        {
            using (var context = ContextFactory.CreateContext())
            {
                var repository = RepositoryFactory.CreatePersonRepository(context);
                var newPerson = new Model.Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Birthday = birthday,
                    Email = email,
                    Phone = phone
                };

                repository.Add(newPerson);

                context.SaveChanges();

                return newPerson;
            }
        }

        public Person UpdatePerson(int personId, 
                                   string firstName, 
                                   string lastName, 
                                   DateTime birthday, 
                                   string email, 
                                   string phone, 
                                   DateTime? lastModified)
        {
            using (var context = ContextFactory.CreateContext())
            {                
                var repository = RepositoryFactory.CreatePersonRepository(context);
                var updatedPerson = new Model.Person
                                    {
                                        PersonId = personId,
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Birthday = birthday,
                                        Email = email,
                                        Phone = phone,
                                        LastModified = lastModified
                                    };
                repository.Update(updatedPerson);

                try
                {
                    context.SaveChanges();
                    return updatedPerson;
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new DataException("Concurrency", ex); 
                }
            }
        }

        public void DeletePerson(int personId, DateTime? lastModified)
        {
            using (var context = ContextFactory.CreateContext())
            {
                var repository = RepositoryFactory.CreatePersonRepository(context);

                repository.Delete(new Model.Person
                {
                    PersonId = personId,
                    LastModified = lastModified
                });

                try
                {
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    var person = repository.Get(personId);

                    if (person != null)
                    {
                        throw new DataException("Concurrency", ex);
                    }
                }
            }
        }
    }
}
