using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using PersonRepository.Service.MyService;

namespace PersonRepository.Service.Test
{
    [TestClass]
    public class UnityServiceRepositoryTest
    {
        //IPersonService _service;
        IUnityContainer Container;

        [TestInitialize]
        public void Setup()
        {
            var people = new List<Person>()
            {
                new Person() { FirstName = "John", LastName = "Smith", Rating = 7, StartDate = DateTime.Parse("01.10.2000") },
                new Person() { FirstName = "Mary", LastName = "Thomas", Rating = 9, StartDate = DateTime.Parse("23.07.1971") }
            };

            var svcMock = new Mock<IPersonService>();
            svcMock.Setup(r => r.GetPeople()).Returns(people);
            svcMock.Setup(r => r.GetPerson(It.IsAny<string>()))
                .Returns((string n) => people.FirstOrDefault(p => p.LastName == n));
            //_service = svcMock.Object;
            Container = new UnityContainer();
            Container.RegisterInstance<IPersonService>(svcMock.Object);
            Container.RegisterType<ServiceRepository>(
                new InjectionProperty("ServiceProxy"));
        }

        [TestMethod]
        public void UnityGetPeople_OnExecute_ReturnsPeople()
        {
            // Arrange
            var repo = Container.Resolve<ServiceRepository>();

            // Act
            var output = repo.GetPeople();

            // Assert 
            Assert.IsNotNull(output);
            Assert.AreEqual(2, output.Count());
        }

        [TestMethod]
        public void UnityGetPerson_OnExecuteWithValidValue_ReturnsPerson()
        {
            // Arrange
            var repo = Container.Resolve<ServiceRepository>();

            // Act
            var output = repo.GetPerson("Smith");

            // Assert 
            Assert.IsNotNull(output);
            Assert.AreEqual("Smith", output.LastName);
        }

        [TestMethod]
        public void UnityGetPerson_OnExecuteWithInvalidValue_ReturnsPerson()
        {
            // Arrange
            var repo = Container.Resolve<ServiceRepository>();

            // Act
            var output = repo.GetPerson("Khandorin");

            // Assert 
            Assert.IsNull(output);
        }
    }
}
