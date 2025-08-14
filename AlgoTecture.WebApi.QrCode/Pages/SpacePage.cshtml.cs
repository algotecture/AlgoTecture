using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations;
using AlgoTecture.Libraries.Spaces.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlgoTecture.WebApi.QrCode.Pages;

public class SpacePageModel : PageModel
{
    private readonly ILogger<SpacePageModel> _logger;
    private readonly IReservationService _reservationService;
    private readonly ISpaceGetter _spaceGetter;

    public IEnumerable<Reservation> TargetReservations { get; set; } = null!;
    public SpaceWithProperty? TargetSpace { get; set; }
    
    public SpacePageModel(ILogger<SpacePageModel> logger, IReservationService reservationService, ISpaceGetter spaceGetter)
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
        TargetReservations = (await _reservationService.GetReservationsBySpaceId(spaceId)).Where(x=>x.ReservationFromUtc >= DateTime.UtcNow);
        
        _logger.LogInformation("Read QR code with spaceId = {TargetSpaceId}", TargetSpace?.Id);
    }
}