using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PersonRepository.Interface;
using System.Collections.Generic;
using PeopleViewer.SharedObjects;
using Moq;
using Microsoft.Practices.Unity;

namespace PeopleViewer.Presentation.Test
{
    [TestClass]
    public class UnityPeopleViewerViewModelTest
    {
        //IPersonRepository _repository;
        IUnityContainer Container;

        [TestInitialize]
        public void Setup()
        {
            var people = new List<Person>()
            {
                new Person() { FirstName = "John", LastName = "Smith", Rating = 7, StartDate = DateTime.Parse("01.10.2000") },
                new Person() { FirstName = "Mary", LastName = "Thomas", Rating = 9, StartDate = DateTime.Parse("23.07.1971") }
            };

            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(r => r.GetPeople()).Returns(people);
            //_repository = repoMock.Object;
            Container = new UnityContainer();
            Container.RegisterInstance<IPersonRepository>(repoMock.Object);
        }

        [TestMethod]
        public void UnityPeople_OnRefreshCommand_Populated()
        {
            // Arrange
            var vm = Container.Resolve<PeopleViewerViewModel>();

            // Act
            vm.RefreshPeopleCommand.Execute(null);

            // Assert
            Assert.IsNotNull(vm.People);
            Assert.AreEqual(2, vm.People.Count());
        }

        [TestMethod]
        public void UnityPeople_OnClearCommand_Empty()
        {
            // Arrange
            var vm = Container.Resolve<PeopleViewerViewModel>();
            vm.RefreshPeopleCommand.Execute(null);
            Assert.AreEqual(2, vm.People.Count(), "Invalid Arrangement");

            // Act
            vm.ClearPeopleCommand.Execute(null);

            // Assert
            Assert.AreEqual(0, vm.People.Count());
        }
    }
}
