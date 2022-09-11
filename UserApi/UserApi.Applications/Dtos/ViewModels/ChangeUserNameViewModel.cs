using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Applications.Dtos.ViewModels
{
    public class ChangeUserNameViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public bool Success { get; set; }
    }
}
