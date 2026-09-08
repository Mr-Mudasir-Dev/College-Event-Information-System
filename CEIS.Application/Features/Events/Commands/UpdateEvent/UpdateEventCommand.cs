using CEIS.Application.Common.Models;
using CEIS.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EventCategory Category { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Venue { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public string? BannerImageUrl { get; set; }
    }
}
