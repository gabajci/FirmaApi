using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaApi.Models
{
    public partial class Employee
    {

        public int Id { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public DateTime SignInDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
    }
}
