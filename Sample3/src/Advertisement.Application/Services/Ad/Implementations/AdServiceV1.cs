using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repositories;
using Advertisement.Application.Services.Ad.Contracts;
using Advertisement.Application.Services.Ad.Contracts.Exceptions;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.User.Interfaces;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Ad.Implementations
{
    public sealed class AdServiceV1 : IAdService
    {
        private readonly IAdRepository _repository;
        private readonly IUserService _userService;

        public AdServiceV1(IUserService userService, IAdRepository repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrent(cancellationToken);
            var ad = new Domain.Ad
            {
                FirstName = request.Name,
                LastName = request.Name,
                Price = request.Price,
                Status = Domain.Ad.Statuses.Created,
                OwnerId = user.Id,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.Save(ad, cancellationToken);

            return new Create.Response
            {
                Id = ad.Id
            };
        }

        public async Task Pay(Pay.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);

            if (ad == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            ad.Status = Domain.Ad.Statuses.Payed;
            ad.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(ad, cancellationToken);
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (ad == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            var user = await _userService.GetCurrent(cancellationToken);
            if (ad.Owner.Id != user.Id)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            ad.Status = Domain.Ad.Statuses.Closed;
            ad.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(ad, cancellationToken);
        }

        public async Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (ad == null)
            {
                throw new AdNotFoundException(request.Id);
            }
            
            return new Get.Response
            {
                Name = $"{ad.FirstName } {ad.LastName }",
                Owner = new Get.Response.OwnerResponse
                {
                    Id = ad.Owner.Id,
                    Name = ad.Owner.Name
                },
                Price = ad.Price,
                Status = ad.Status.ToString()
            };
        }

        public async Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(
                cancellationToken
            );

            if (total == 0)
            {
                return new GetPaged.Response
                {
                    Items = Array.Empty<GetPaged.Response.AdResponse>(),
                    Total = total,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }
            
            var ads = await _repository.GetPaged(
                request.Offset, request.Limit, cancellationToken
            );


            return new GetPaged.Response
            {
                Items = ads.Select(ad => new GetPaged.Response.AdResponse
                {
                    Id = ad.Id,
                    Name = $"{ad.FirstName} {ad.LastName}",
                    Price = ad.Price,
                    Status = ad.Status.ToString()
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}