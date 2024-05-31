using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.InfraService
{
    public interface IJwtTokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateToken();
    }
}
