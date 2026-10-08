using CEIS.Application.Common.Models;
using CEIS.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<Result<int>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EventCategory Category { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Venue { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public IFormFile? BannerImage { get; set; }

        // Body se nahi aayega, Controller JWT se set karega
        public string OrganizerId { get; set; } = string.Empty;
    }
}
