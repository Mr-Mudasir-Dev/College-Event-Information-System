using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface ICertificateGenerator
    {
        byte[] GenerateCertificate(string studentName, string eventTitle, DateOnly eventDate);
    }
}
