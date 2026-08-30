using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Queries
{
    public class CurrentUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? EnrollmentNo { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
