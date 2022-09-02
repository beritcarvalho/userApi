using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Applications.InputModels
{
    public class UpdatePersonInputModel
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public DateTime? Inactive_Date { get; set; }
    }
}
