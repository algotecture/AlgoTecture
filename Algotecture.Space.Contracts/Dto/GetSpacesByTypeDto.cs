namespace Algotecture.Space.Contracts.Dto;

public record GetSpacesByTypeDto(
    long Id,
    long? ParentId,
    string? ParentName,
    int SpaceTypeId,
    string SpaceTypeName,
    string? SpaceAddress,
    double Latitude,
    double Longitude,
    double Area,
    string Name,
    string? Description,
    string? SpaceProperties,
    DateTime CreatedAt,
    bool IsDeleted,
    List<SpaceImageDto> Images);

public record SpaceImageDto(
    long Id,
    string Url,
    string Path,
    string? ContentType,
    DateTime CreatedAt);