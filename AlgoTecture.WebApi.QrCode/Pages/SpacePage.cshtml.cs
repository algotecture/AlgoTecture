using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Spaces.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlgoTecture.WebApi.QrCode.Pages;

public class SpacePageModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IReservationService _reservationService;
    private readonly ISpaceGetter _spaceGetter;

    public IEnumerable<Reservation> TargetReservations { get; set; }
    public SpaceWithProperty TargetSpace { get; set; }
    
    public SpaceProperty TargetsSpaceProperty { get; set; }

    public SpacePageModel(ILogger<IndexModel> logger, IReservationService reservationService, ISpaceGetter spaceGetter)
    {
        _logger = logger;
        _reservationService = reservationService;
        _spaceGetter = spaceGetter;
    }

    public async Task OnGet()
    {
        int spaceId = 1;
        var data = Request.Query["spaceId"];
        var isValid = int.TryParse(data, out var value);

        if (isValid)
        {
            spaceId = value;
        }
        TargetSpace = await _spaceGetter.GetByIdWithProperty(spaceId);
        TargetReservations = await _reservationService.GetReservationsBySpaceId(spaceId);
    }
}