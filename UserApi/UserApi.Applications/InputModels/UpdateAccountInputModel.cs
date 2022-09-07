using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Applications.InputModels
{
    public class UpdateAccountInputModel : AccountInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
