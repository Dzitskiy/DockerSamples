using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Advertisement.Application
{
    public interface IClaimsAccessor
    {
        Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellationToken);
    }
}