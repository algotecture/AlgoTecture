using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.Reservations;
using Algotecture.Libraries.Spaces.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algotecture.WebApi.QrCode.Pages;

public class IndexModel : PageModel
{

    private readonly IReservationService _reservationService;
    private readonly ISpaceGetter _spaceGetter;

    public IEnumerable<Reservation> TargetReservations { get; set; } = null!;
    public Space? TargetSpace { get; set; }

    public IndexModel(IReservationService reservationService, ISpaceGetter spaceGetter)
    {
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