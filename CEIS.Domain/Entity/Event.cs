using CEIS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Entity
{
    public class Event : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EventCategory Category { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Venue { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public string? BannerImageUrl { get; set; }
        public EventStatus Status { get; set; } = EventStatus.Pending;

        public string OrganizerId { get; set; } = string.Empty;
    }
}
