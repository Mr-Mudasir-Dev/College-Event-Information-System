using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Venue { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public string? BannerImageUrl { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
