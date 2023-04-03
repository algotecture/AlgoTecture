using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Spaces.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlgoTecture.WebApi.QrCode.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IReservationService _reservationService;
    private readonly ISpaceGetter _spaceGetter;

    public IEnumerable<Reservation> TargetReservations { get; set; }
    public Space TargetSpace { get; set; }
    
    public SpaceProperty TargetsSpaceProperty { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IReservationService reservationService, ISpaceGetter spaceGetter)
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
        TargetSpace = await _spaceGetter.GetById(spaceId);
        TargetReservations = await _reservationService.GetReservationsBySpaceId(spaceId);
    }
}