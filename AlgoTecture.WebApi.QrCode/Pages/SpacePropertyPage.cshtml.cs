using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.GeoAdminSearch.Models;
using AlgoTecture.WebApi.QrCode.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlgoTecture.WebApi.QrCode.Pages;

public class SpacePropertyPage : PageModel
{
    private readonly IGeoAdminSearcher _geoAdminSearcher;

    public GeoAdminBuildingViewModel GeoAdminBuildingView{ get; set; } = null!;

    public string LabelAddress { get; set; } = null!;


    public SpacePropertyPage(IGeoAdminSearcher geoAdminSearcher)
    {
        _geoAdminSearcher = geoAdminSearcher;
    }

    public async Task OnGet()
    {
        var featureId = 1;
        var featureIdStr = Request.Query["featureId"];
        var labelStr = Request.Query["label"];
        var isValidFeatureIdStr = int.TryParse(featureIdStr, out var valueFeatureId);

        if (isValidFeatureIdStr)
        {
            featureId = valueFeatureId;
        }

        LabelAddress = labelStr!;

        var buildingProperties = await _geoAdminSearcher.GetBuildingModel(new GeoAdminSearchBuildingModel()
        {
            Latitude = 0,
            Longitude = 0,
            FeatureId = featureId
        });

        if (buildingProperties == null)
        {
            throw new ArgumentNullException($"$Nothing found with featureId ={featureId}");
        }

        const string isUnknownStr = "is unknown";
        var formattedGeoAdminBuilding = new GeoAdminBuildingViewModel
        {
        MunicipalityName = string.IsNullOrEmpty(buildingProperties.MunicipalityName) ? $"municipality name {isUnknownStr}" : buildingProperties.MunicipalityName,
        MunicipalityId =  buildingProperties.MunicipalityId == 0 ? $"municipality id {isUnknownStr}" : buildingProperties.MunicipalityId.ToString(),
        PlaceName = string.IsNullOrEmpty(buildingProperties.PlaceName) ? $"place name {isUnknownStr}" : buildingProperties.PlaceName,
        BuildingName = string.IsNullOrEmpty(buildingProperties.BuildingName) ? $"building name {isUnknownStr}" : buildingProperties.BuildingName,
        BuildingYear = buildingProperties.BuildingYear == 0 ? $"building year {isUnknownStr}" : buildingProperties.BuildingYear.ToString(),
        BuildingCategory = string.IsNullOrEmpty(buildingProperties.BuildingCategory) ? $"building category {isUnknownStr}" : buildingProperties.BuildingCategory,
        BuildingClass = string.IsNullOrEmpty(buildingProperties.BuildingClass) ? $"building class {isUnknownStr}" : buildingProperties.BuildingClass,
        Levels = buildingProperties.Levels == 0 ? $"levels {isUnknownStr}" : buildingProperties.Levels.ToString(),
        Area = buildingProperties.Area == 0 ? $"area {isUnknownStr}" : buildingProperties.Area.ToString(),
        FloorArea = buildingProperties.FloorArea == 0 ? $"floor area {isUnknownStr}" : buildingProperties.FloorArea.ToString(),
        Flats = buildingProperties.Flats == 0 ? $"flats {isUnknownStr}" : buildingProperties.Flats.ToString(),
        };

        GeoAdminBuildingView = formattedGeoAdminBuilding;
    }
}