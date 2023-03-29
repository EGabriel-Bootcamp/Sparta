using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Management.Domain.ViewModel
{
    public class UserFilter
    {
        public string Keyword { get; set; } = string.Empty;
        public string Filter { get; set; } = string.Empty;

    }


    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string MartialStatus { get; set; } = string.Empty;
        public string StateOfResident { get; set; } = string.Empty;
    }

}
