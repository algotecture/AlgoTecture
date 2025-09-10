using Algotecture.Space.Contracts.Dto;
using Algotecture.Space.Contracts.Queries;
using Algotecture.Space.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Algotecture.Space.Application.Handlers;

public class GetSpacesByTypeQueryHandler: IRequestHandler<GetSpacesByTypeQuery, List<GetSpacesByTypeDto>>
{
    private readonly SpaceDbContext _db;

    public GetSpacesByTypeQueryHandler(SpaceDbContext db)
    {
        _db = db;
    }

    public async Task<List<GetSpacesByTypeDto>> Handle(GetSpacesByTypeQuery request, CancellationToken cancellationToken)
    {
        return await _db.Spaces
            .Where(space => space.SpaceTypeId == request.SpaceTypeId)
            .Include(space => space.SpaceType)
            .Include(space => space.Images)
            .Include(space => space.Parent)
            .Select(space => new GetSpacesByTypeDto(
                space.Id,
                space.ParentId,
                space.Parent != null ? space.Parent.Name : null,
                space.SpaceTypeId,
                space.SpaceType!.Name,
                space.SpaceAddress,
                space.Latitude,
                space.Longitude,
                space.Area,
                space.Name ?? "",
                space.Description,
                space.SpaceProperties,
                space.CreatedAt,
                space.IsDeleted,
                space.Images.Select(image => new SpaceImageDto(
                    image.Id,
                    image.Url,
                    image.Path,
                    image.ContentType,
                    image.CreatedAt
                )).ToList()
            ))
            .ToListAsync(cancellationToken);
    }
}
