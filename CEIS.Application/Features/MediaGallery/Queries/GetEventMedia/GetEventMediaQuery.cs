using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Queries.GetEventMedia
{
    public class GetEventMediaQuery : IRequest<Result<IEnumerable<MediaDto>>>
    {
        public int EventId { get; set; }
    }
}
