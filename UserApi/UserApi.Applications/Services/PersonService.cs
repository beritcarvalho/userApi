using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.InputModels;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{   
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonViewModel> AddPerson(PersonInputModel personInput)
        {
            var person = new Person();
            person.First_Name = personInput.First_Name;
            person.Last_Name = personInput.Last_Name;
            person.Cpf = personInput.Cpf;
            person.Phone = personInput.Phone;
            person.Email = personInput.Email;
            person.Create_Date = DateTime.Now;
            person.Active = true;

            var teste = await _personRepository.InsertAsync(person);

            var personViewModel = new PersonViewModel
            {
                Id = teste.Id,
                First_Name = teste.First_Name,
                Last_Name = teste.Last_Name,
                Active = teste.Active
            };

            return personViewModel;
        }

        public async Task<PersonViewModel> DisablePerson(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            person.Active = false;
            person.Inactive_Date = DateTime.Now;
            person.Change_Date = DateTime.Now;

            await _personRepository.UpdateAsync(person);

            var personView = new PersonViewModel
            {
                Id = person.Id,
                First_Name = person.First_Name,
                Last_Name = person.Last_Name,
                Create_Date = person.Create_Date,
                Active = person.Active,
                Inactive_Date = person.Inactive_Date,
                Activation_Date = person.Activation_Date,
            };
            return personView;
        }

        public async Task<PersonViewModel> ActivatePerson(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            person.Active = true;
            person.Activation_Date = DateTime.Now;
            person.Change_Date = DateTime.Now;

            await _personRepository.UpdateAsync(person);

            var personView = new PersonViewModel
            {
                Id = person.Id,
                First_Name = person.First_Name,
                Last_Name = person.Last_Name,
                Create_Date = person.Create_Date,
                Active = person.Active,
                Inactive_Date = person.Inactive_Date,
                Activation_Date = person.Activation_Date,
                Change_Date = person.Change_Date,
            };
            return personView;
        }

        public async Task<PersonViewModel> GetById(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            
            if (person == null)
                return null;

            var personView = new PersonViewModel
            {
                Id = person.Id,
                First_Name = person.First_Name
            };

            return personView;
        }
    }
}
