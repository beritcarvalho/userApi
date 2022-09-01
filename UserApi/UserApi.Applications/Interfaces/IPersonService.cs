using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IPersonService
    {
        Task<PersonViewModel> GetById(int id);
    }
}
