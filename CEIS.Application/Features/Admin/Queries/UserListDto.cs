using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Queries
{
    public class UserListDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? EnrollmentNo { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public bool IsLockedOut { get; set; }
    }
}
