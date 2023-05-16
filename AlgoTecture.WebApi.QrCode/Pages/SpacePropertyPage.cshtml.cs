using AlgoTecture.Libraries.GeoAdminSearch;
using AlgoTecture.Libraries.GeoAdminSearch.Models;
using AlgoTecture.Libraries.GeoAdminSearch.Models.GeoAdminModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlgoTecture.WebApi.QrCode.Pages;

public class SpacePropertyPage : PageModel
{
    private readonly ILogger<SpacePageModel> _logger;
    private readonly IGeoAdminSearcher _geoAdminSearcher;

    public GeoAdminBuilding GeoAdminBuilding{ get; set; }
    

    public SpacePropertyPage(ILogger<SpacePageModel> logger, IGeoAdminSearcher geoAdminSearcher)
    {
        _logger = logger;
        _geoAdminSearcher = geoAdminSearcher;
    }

    public async Task OnGet()
    {
        int featureId = 1;
        var data = Request.Query["featureId"];
        var isValid = int.TryParse(data, out var value);

        if (isValid)
        {
            featureId = value;
        }

        var buildingProperties = await _geoAdminSearcher.GetBuildingModel(new GeoAdminSearchBuildingModel()
        {
            Latitude = 0,
            Longitude = 0,
            FeatureId = featureId
        });

        GeoAdminBuilding = buildingProperties ?? throw new ArgumentNullException($"$Nothing found with featureId ={featureId}");
        
        
    }
}