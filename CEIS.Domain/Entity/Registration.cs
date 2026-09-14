using CEIS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Entity
{
    public class Registration : BaseEntity
    {
        public int EventId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Confirmed;
    }
}
