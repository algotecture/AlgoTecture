using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.GeoAdminSearch.Models;
using AlgoTecture.WebApi.QrCode.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlgoTecture.WebApi.QrCode.Pages;

public class SpacePropertyPage : PageModel
{
    private readonly ILogger<SpacePageModel> _logger;
    private readonly IGeoAdminSearcher _geoAdminSearcher;

    public GeoAdminBuildingViewModel GeoAdminBuildingView{ get; set; }

    public string LabelAddress { get; set; }
    

    public SpacePropertyPage(ILogger<SpacePageModel> logger, IGeoAdminSearcher geoAdminSearcher)
    {
        _logger = logger;
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

        LabelAddress = labelStr;

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
        var formattedGeoAdminBuilding = new GeoAdminBuildingViewModel()
        {
        MunicipalityName = string.IsNullOrEmpty(buildingProperties.MunicipalityName) ? $"municipality name {isUnknownStr}" : buildingProperties.MunicipalityName,
        MunicipalityId =  buildingProperties.MunicipalityId == default ? $"municipality id {isUnknownStr}" : buildingProperties.MunicipalityId.ToString(),
        PlaceName = string.IsNullOrEmpty(buildingProperties.PlaceName) ? $"place name {isUnknownStr}" : buildingProperties.PlaceName,
        BuildingName = string.IsNullOrEmpty(buildingProperties.BuildingName) ? $"building name {isUnknownStr}" : buildingProperties.BuildingName,
        BuildingYear = buildingProperties.BuildingYear == default ? $"building year {isUnknownStr}" : buildingProperties.BuildingYear.ToString(),
        BuildingCategory = string.IsNullOrEmpty(buildingProperties.BuildingCategory) ? $"building category {isUnknownStr}" : buildingProperties.BuildingCategory,
        BuildingClass = string.IsNullOrEmpty(buildingProperties.BuildingClass) ? $"building class {isUnknownStr}" : buildingProperties.BuildingClass,
        Levels = buildingProperties.Levels == default ? $"levels {isUnknownStr}" : buildingProperties.Levels.ToString(),
        Area = buildingProperties.Area == default ? $"area {isUnknownStr}" : buildingProperties.Area.ToString(),
        FloorArea = buildingProperties.FloorArea == default ? $"floor area {isUnknownStr}" : buildingProperties.FloorArea.ToString(),
        Flats = buildingProperties.Flats == default ? $"flats {isUnknownStr}" : buildingProperties.Flats.ToString(),
        };

        GeoAdminBuildingView = formattedGeoAdminBuilding;
    }
}