using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Commands.DeleteMedia
{
    public class DeleteMediaCommand : IRequest<Result<string>>
    {
        public int MediaId { get; set; }
        public string RequestedBy { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
