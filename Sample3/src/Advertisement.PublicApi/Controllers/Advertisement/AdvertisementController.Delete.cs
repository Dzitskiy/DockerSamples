using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Ad.Contracts;
using Advertisement.PublicApi.Controllers.User;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers.Advertisement
{
    public partial class AdvertisementController
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _adService.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}