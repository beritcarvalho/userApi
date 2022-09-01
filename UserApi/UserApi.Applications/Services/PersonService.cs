using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Interfaces;
using UserApi.Applications.ViewModels;
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
