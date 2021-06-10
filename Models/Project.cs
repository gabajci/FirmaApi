using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaApi.Models
{
    public partial class Project
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int LeaderId { get; set; }
        public int DivisionId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
