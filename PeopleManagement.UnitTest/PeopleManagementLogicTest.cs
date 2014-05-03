using Moq;
using NUnit.Framework;
using PeopleManagement.Domain.Services;
using PeopleManagement.Domain.Model;
using PeopleManagement.UnitTest.Mock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.UnitTest
{
    [TestFixture]
    public class PeopleManagementLogicTest
    {
        
        [Test]
        public void TestList()
        {
            var logic = CreateLogic();
            var userList = logic.List(0, 10).ToList();

            Assert.AreEqual(6, userList.Count);
            for (var i = 1; i <= userList.Count(); i++)
            {
                var user = userList[i - 1];
                Assert.AreEqual("User" + i, user.FirstName);
                Assert.AreEqual("Test" + i, user.LastName);
            }

            userList = logic.List(0, 4).ToList();
            Assert.AreEqual(4, userList.Count);

            userList = logic.List(1, 4).ToList();
            Assert.AreEqual(2, userList.Count);

            userList = logic.List(2, 4).ToList();
            Assert.AreEqual(0, userList.Count);

            try
            {
                userList = logic.List(0, 0).ToList();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
                Assert.AreEqual(ex.Message, "Page size");
            }
        }

        [Test]
        public void TestCountPage()
        {
            var logic = CreateLogic();

            var pageCount = logic.CountPage(10);
            Assert.AreEqual(1, pageCount);

            pageCount = logic.CountPage(1);
            Assert.AreEqual(6, pageCount);

            pageCount = logic.CountPage(2);
            Assert.AreEqual(3, pageCount);

            pageCount = logic.CountPage(3);
            Assert.AreEqual(2, pageCount);

            pageCount = logic.CountPage(4);
            Assert.AreEqual(2, pageCount);

            try
            {
                pageCount = logic.CountPage(0);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
                Assert.AreEqual(ex.Message, "Page size");
            }
        }

        [Test]
        public void TestFindPerson()
        {
            var logic = CreateLogic();

            var person = logic.FindPerson(1);
            Assert.NotNull(person);
            Assert.AreEqual("User1", person.FirstName);
            Assert.AreEqual("Test1", person.LastName);

            person = logic.FindPerson(7);
            Assert.IsNull(person);
        }

        [Test]
        public void TestAdd()
        {
            var logic = CreateLogic();
            MockPeopleContextFactory.Refresh();
            logic.AddPerson("User7", "Test7", DateTime.Now, "user7@email.com", "12345678907");

            MockPeopleContextFactory.MockSet.Verify(s => s.Add(It.IsAny<Person>()), Times.Once());

            MockPeopleContextFactory.Refresh();
        }

        [Test]
        public void TestUpdate()
        {
            var logic = CreateLogic();
            var person = logic.FindPerson(1);
            MockPeopleContextFactory.Refresh();
            logic.UpdatePerson(person.PersonId, "UpdateUser1", person.LastName, person.Birthday, person.Email, person.Phone, person.LastModified.Value);

            MockPeopleContextFactory.MockContext.Verify(c => c.SaveChanges(), Times.Once());           

            try
            {
                MockPeopleContextFactory.Refresh();
                logic.UpdatePerson(person.PersonId, "UpdateUser1", person.LastName, person.Birthday, person.Email, person.Phone, DateTime.Now.AddSeconds(1));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<DataException>(ex);
                Assert.AreEqual("Concurrency", ex.Message);
            }

            MockPeopleContextFactory.Refresh();
        }

        [Test]
        public void TestDelete()
        {
            var logic = CreateLogic();
            var person = logic.FindPerson(1);
            MockPeopleContextFactory.Refresh();
            logic.DeletePerson(person.PersonId, person.LastModified.Value);
            MockPeopleContextFactory.MockContext.Verify(c => c.SaveChanges(), Times.Once());
            
            try
            {
                MockPeopleContextFactory.Refresh();
                logic.DeletePerson(person.PersonId, DateTime.Now.AddSeconds(1));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<DataException>(ex);
                Assert.AreEqual("Concurrency", ex.Message);
            }

            MockPeopleContextFactory.Refresh();
        }

        private IPeopleManagementService CreateLogic()
        {
            return DIContainer.GetService<IPeopleManagementService>();
        }
    }
}
