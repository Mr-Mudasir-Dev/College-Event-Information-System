using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(string userId, string email, string fullName, IList<string> roles);
    }
}
