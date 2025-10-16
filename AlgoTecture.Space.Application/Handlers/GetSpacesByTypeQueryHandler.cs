using AlgoTecture.Space.Contracts.Dto;
using AlgoTecture.Space.Contracts.Queries;
using AlgoTecture.Space.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlgoTecture.Space.Application.Handlers;

public class GetSpacesByTypeQueryHandler: IRequestHandler<GetSpacesByTypeQuery, List<SpaceDto>>
{
    private readonly SpaceDbContext _db;

    public GetSpacesByTypeQueryHandler(SpaceDbContext db)
    {
        _db = db;
    }

    public async Task<List<SpaceDto>> Handle(GetSpacesByTypeQuery request, CancellationToken cancellationToken)
    {
        return await _db.Spaces
            .Where(space => space.SpaceTypeId == request.SpaceTypeId)
            .Include(space => space.SpaceType)
            .Include(space => space.Images)
            .Include(space => space.Parent)
            .Select(space => new SpaceDto(
                space.Id,
                space.ParentId,
                space.Parent != null ? space.Parent.Name : null,
                space.SpaceTypeId,
                space.SpaceType.Name,
                space.SpaceAddress,
                space.Location != null ? space.Location.Y : null, //latitude
                space.Location != null ? space.Location.X : null, //longitude
                space.Area,
                space.Name ?? "",
                space.Description,
                space.SpaceProperties,
                space.DataSource,
                space.CreatedAt,
                space.IsDeleted,
                space.Images.Select(image => new SpaceImageDto(
                    image.Id,
                    image.Url,
                    image.Path,
                    image.ContentType,
                    image.CreatedAt
                )).ToList(),
                null
            ))
            .ToListAsync(cancellationToken);
    }
}
