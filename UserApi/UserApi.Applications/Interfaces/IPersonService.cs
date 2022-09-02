using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.InputModels;
using UserApi.Applications.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IPersonService
    {
        Task<PersonViewModel> GetById(int id);
        Task<PersonViewModel> AddPerson(PersonInputModel person);
        Task<PersonViewModel> DisablePerson(int id);
        Task<PersonViewModel> ActivatePerson(int id);
    }
}
