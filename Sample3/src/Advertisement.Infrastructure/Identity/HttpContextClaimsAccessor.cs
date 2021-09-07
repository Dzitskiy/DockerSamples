using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application;
using Microsoft.AspNetCore.Http;

namespace Advertisement.Infrastructure.Identity
{
    public sealed class HttpContextClaimsAccessor : IClaimsAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextClaimsAccessor(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

        public async Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellationToken) => _contextAccessor.HttpContext.User.Claims;
    }
}