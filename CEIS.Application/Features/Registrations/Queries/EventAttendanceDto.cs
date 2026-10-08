using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Queries
{
    public class EventAttendanceDto
    {
        public int RegistrationId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? EnrollmentNo { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime RegisteredOn { get; set; }
        public bool IsAttended { get; set; }          // naya field
        public DateTime? AttendedAt { get; set; }
    }
}
