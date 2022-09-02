using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Applications.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Create_Date { get; set; }
        public bool Active { get; set; }
        public DateTime? Inactive_Date { get; set; }
        public DateTime? Activation_Date { get; set; }
        public DateTime? Change_Date { get; set; }
    }
}
