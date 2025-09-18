using AlgoTecture.Space.Domain;

namespace AlgoTecture.Space.Contracts.Dto;

public record SpaceDto(
    long Id,
    long? ParentId,
    string? ParentName,
    int SpaceTypeId,
    string SpaceTypeName,
    string? SpaceAddress,
    double? Latitude,
    double? Longitude,
    decimal? Area,
    string Name,
    string? Description,
    string? SpaceProperties,
    string? DataSource,
    DateTime CreatedAt,
    bool IsDeleted,
    List<SpaceImageDto> Images);

public record SpaceImageDto(
    long Id,
    string? Url,
    string? Path,
    string? ContentType,
    DateTime CreatedAt);