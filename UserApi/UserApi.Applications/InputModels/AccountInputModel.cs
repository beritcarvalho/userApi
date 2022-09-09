using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserApi.Applications.InputModels
{
    public class AccountInputModel
    {


        public int? Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }


        public string Cpf { get; set; }

        private PhoneInputModel phone;

        public PhoneInputModel Phone
        {
            get 
            {
                phone.Ddd = phone.Ddd.TrimStart('0');                
                return phone;
            }
            set { phone = value; }
        }

        public string? Email { get; set; }
    }
}
