using Moq;
using PeopleManagement.Domain;
using PeopleManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.UnitTest.Mock
{
    /// <summary>
    /// PeopleContext factory which provide Mock PeopleContext
    /// </summary>
    internal class MockPeopleContextFactory : IPeopleContextFactory
    {
        private static Mock<PeopleContext> _mockContext;
        private static Mock<IDbSet<Person>> _mockSet;

        /// <summary>
        /// Mock DbSet
        /// </summary>
        public static Mock<IDbSet<Person>> MockSet
        {
            get { return MockPeopleContextFactory._mockSet; }
        }

        /// <summary>
        /// Mock PeopleContext
        /// </summary>
        public static Mock<PeopleContext> MockContext
        {
            get { return MockPeopleContextFactory._mockContext; }
        }

        /// <summary>
        /// Creates PeopleContext from Mock
        /// </summary>
        /// <returns></returns>
        public Domain.Model.PeopleContext CreateContext()
        {
            _mockSet = _mockSet ?? CreateMockSet();
            _mockContext = _mockContext ?? CreateMockContext(_mockSet);
            return _mockContext.Object;
        }

        /// <summary>
        /// Refreshes static Mock objects
        /// </summary>
        public static void Refresh()
        {
            _mockSet = CreateMockSet();
            _mockContext = CreateMockContext(_mockSet);
        }

        /// <summary>
        /// Creates a mock DbSet which contians hardcoded data
        /// </summary>
        /// <returns></returns>
        private static Mock<IDbSet<Person>> CreateMockSet()
        {
            // Hardcoded data
            var data = new List<Person>
            {
                new Person { PersonId = 1, FirstName = "User1", LastName = "Test1", Birthday= DateTime.Now, Email = "user1@email.com", Phone = "12345678901" , LastModified = DateTime.UtcNow},
                new Person { PersonId = 2, FirstName = "User2", LastName = "Test2", Birthday= DateTime.Now, Email = "user2@email.com", Phone = "12345678902" , LastModified = DateTime.UtcNow},
                new Person { PersonId = 3, FirstName = "User3", LastName = "Test3", Birthday= DateTime.Now, Email = "user3@email.com", Phone = "12345678903" , LastModified = DateTime.UtcNow},
                new Person { PersonId = 4, FirstName = "User4", LastName = "Test4", Birthday= DateTime.Now, Email = "user4@email.com", Phone = "12345678904" , LastModified = DateTime.UtcNow},
                new Person { PersonId = 5, FirstName = "User5", LastName = "Test5", Birthday= DateTime.Now, Email = "user5@email.com", Phone = "12345678905" , LastModified = DateTime.UtcNow},
                new Person { PersonId = 6, FirstName = "User6", LastName = "Test6", Birthday= DateTime.Now, Email = "user6@email.com", Phone = "12345678906" , LastModified = DateTime.UtcNow},
            }.AsQueryable();

            // set up mock set
            var mockSet = new Mock<IDbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(1)).Returns(data.First());

            return mockSet;
        }

        /// <summary>
        /// Creates Mock PeopleContext with given Mock set
        /// </summary>
        /// <param name="mockSet"></param>
        /// <returns></returns>
        private static Mock<PeopleContext> CreateMockContext(Mock<IDbSet<Person>> mockSet)
        {
            // Set up mock context
            var mockContext = new Mock<PeopleContext>();
            mockContext.Setup(c => c.People).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            return mockContext;
        }
    }
}
