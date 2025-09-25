using AlgoTecture.Space.Domain;

namespace AlgoTecture.Space.Contracts.Dto;

public record SpaceDto(
    Guid Id,
    Guid? ParentId,
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
    Guid Id,
    string? Url,
    string? Path,
    string? ContentType,
    DateTime CreatedAt);