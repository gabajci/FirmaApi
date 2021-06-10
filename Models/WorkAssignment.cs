using System;
using System.Collections.Generic;

#nullable disable

namespace FirmaApi.Models
{
    public partial class WorkAssignment
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
