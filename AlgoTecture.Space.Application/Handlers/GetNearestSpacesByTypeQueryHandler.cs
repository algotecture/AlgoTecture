using AlgoTecture.Space.Contracts.Dto;
using AlgoTecture.Space.Contracts.Queries;
using AlgoTecture.Space.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AlgoTecture.Space.Application.Handlers;

public class GetNearestSpacesByTypeQueryHandler : IRequestHandler<GetNearestSpacesByTypeQuery, List<SpaceDto>>
{
    private readonly SpaceDbContext _db;

    public GetNearestSpacesByTypeQueryHandler(SpaceDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<SpaceDto>> Handle(GetNearestSpacesByTypeQuery request, CancellationToken cancellationToken)
    {
        double longitude = request.Longitude;
        double latitude = request.Latitude;
        int spaceTypeId = request.SpaceTypeId;
        int limit = request.Count;
        double maxDistanceMeters = request.MaxDistanceMeters;

        //todo check result on real data
        var result = await _db.Set<Domain.Space>().FromSqlInterpolated($@"
    WITH filtered_spaces AS (
        SELECT s.""Id"",
               s.""ParentId"",
               p.""Name"" AS ""ParentName"",
               s.""SpaceTypeId"",
               st.""Name"" AS ""SpaceTypeName"",
               s.""SpaceAddress"",
               s.""Location"",
               ST_Y(s.""Location""::geometry) AS ""Latitude"",
               ST_X(s.""Location""::geometry) AS ""Longitude"",
               s.""Area"",
               s.""Name"",
               s.""Description"",
               s.""SpaceProperties"",
               s.""DataSource"",
               s.""CreatedAt"",
               s.""IsDeleted""
        FROM ""Spaces"" s
        LEFT JOIN ""Spaces"" p ON p.""Id"" = s.""ParentId""
        LEFT JOIN ""SpaceTypes"" st ON st.""Id"" = s.""SpaceTypeId""
        LEFT JOIN ""SpaceImages"" i ON i.""SpaceId"" = s.""Id""
        WHERE s.""SpaceTypeId"" = {spaceTypeId}
          AND ST_DWithin(
                s.""Location""::geography,
                ST_MakePoint({longitude}, {latitude})::geography,
                {maxDistanceMeters}
          )
        ORDER BY s.""Location""::geography <-> ST_MakePoint({longitude}, {latitude})::geography
        LIMIT {limit}
    )
    SELECT * FROM filtered_spaces
").ToListAsync(cancellationToken: cancellationToken);
        var res = result.Select(space => new SpaceDto(space.Id,
            space.ParentId,
            space.Parent?.Name,
            space.SpaceTypeId,
            "",
            space.SpaceAddress,
            space.Location != null ? space.Location.Y : null,
            space.Location != null ? space.Location.X : null,
            space.Area,
            space.Name ?? "",
            space.Description,
            space.SpaceProperties,
            space.DataSource,
            space.CreatedAt,
            space.IsDeleted,
            space.Images.Count != 0
                ? space.Images.Select(image => new SpaceImageDto(
                    image.Id,
                    image.Url,
                    image.Path,
                    image.ContentType,
                    image.CreatedAt
                )).ToList()
                : [])
        ).ToList();

        return res;
    }
}