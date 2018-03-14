using System.Collections.Generic;
using PeopleViewer.SharedObjects;

namespace PersonRepository.Interface
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPeople();

        Person GetPerson(string lastName);

        void AddPerson(Person newPerson);

        void UpdatePeople(IEnumerable<Person> updatedPeople);

        void UpdatePerson(string lastName, Person updatedPerson);

        void DeletePerson(string lastName);

    }
}
