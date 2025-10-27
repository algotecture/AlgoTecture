using AlgoTecture.Space.Contracts.Dto;
using AlgoTecture.Space.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using GetNearestSpacesByTypeQuery = AlgoTecture.Space.Application.Queries.GetNearestSpacesByTypeQuery;

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
        var result = await _db.Database.SqlQueryRaw<SpaceSqlProjection>($@"
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
               s.""IsDeleted"",
               ST_Distance(s.""Location""::geography, ST_MakePoint(@lat, @lng)::geography) AS ""DistanceMeters""
        FROM ""Spaces"" s
        LEFT JOIN ""Spaces"" p ON p.""Id"" = s.""ParentId""
        LEFT JOIN ""SpaceTypes"" st ON st.""Id"" = s.""SpaceTypeId""
        LEFT JOIN ""SpaceImages"" i ON i.""SpaceId"" = s.""Id""
        WHERE s.""SpaceTypeId"" = {spaceTypeId}
          AND ST_DWithin(
                s.""Location""::geography,
                ST_MakePoint(@lat, @lng)::geography,
                {maxDistanceMeters}
          )
        ORDER BY s.""Location""::geography <-> ST_MakePoint(@lat, @lng)::geography
        LIMIT {limit}
    )
    SELECT * FROM filtered_spaces
", new NpgsqlParameter("@lng", request.Longitude),
            new NpgsqlParameter("@lat", request.Latitude)).ToListAsync(cancellationToken: cancellationToken);
        var res = result.Select(space => new SpaceDto(
            space.Id,
            space.ParentId,
            space.ParentName,
            space.SpaceTypeId,
            space.SpaceTypeName ?? "",
            space.SpaceAddress,
            space.Latitude,
            space.Longitude,
            space.Area,
            space.Name ?? "",
            space.Description,
            space.SpaceProperties,
            space.DataSource,
            space.CreatedAt,
            space.IsDeleted,
            new List<SpaceImageDto>(),
            space.DistanceMeters
        )).ToList();

        return res;
    }
}

internal class SpaceSqlProjection
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string? ParentName { get; set; }
    public int SpaceTypeId { get; set; }
    public string? SpaceTypeName { get; set; }
    public string? SpaceAddress { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public decimal? Area { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SpaceProperties { get; set; }
    public string? DataSource { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public double DistanceMeters { get; set; }
}