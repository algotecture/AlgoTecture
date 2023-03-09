using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Libraries.Reservation.Models;

namespace AlgoTecture.Libraries.Reservation;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Domain.Models.RepositoryModels.Reservation> AddOrUpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        if (addOrUpdateReservationModel == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel));
        if (addOrUpdateReservationModel.ReservationFrom == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationFrom));
        if (addOrUpdateReservationModel.ReservationTo == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationTo));
        
        var entity = new Domain.Models.RepositoryModels.Reservation()
        {
            Id = addOrUpdateReservationModel.ReservationId ?? default,
            PriceCurrency = addOrUpdateReservationModel.PriceCurrency,
            ReservationFrom = addOrUpdateReservationModel.ReservationFrom,
            ReservationTo = addOrUpdateReservationModel.ReservationTo,
            ReservationStatus = addOrUpdateReservationModel.ReservationStatus,
            TotalPrice = addOrUpdateReservationModel.TotalPrice,
            SpaceId = addOrUpdateReservationModel.SpaceId,
            SubSpaceId = addOrUpdateReservationModel.SubSpaceId,
            TenantUserId = addOrUpdateReservationModel.TenantUserId,
            ReservationDateTime = addOrUpdateReservationModel.ReservationDateTime,
            PriceSpecificationId = addOrUpdateReservationModel.PriceSpecificationId
        };
        if (addOrUpdateReservationModel.ReservationId != null)
        {
            var targetReservation = await _unitOfWork.Reservations.GetById(addOrUpdateReservationModel.ReservationId.Value);
            if (targetReservation != null)
            {
                return await _unitOfWork.Reservations.Upsert(entity);
            }
        }
        else
        {
            var reservation = await _unitOfWork.Reservations.CheckReservation(addOrUpdateReservationModel.SpaceId, addOrUpdateReservationModel.SubSpaceId,
                addOrUpdateReservationModel.ReservationFrom.Value, addOrUpdateReservationModel.ReservationTo.Value);

            if (reservation == null)
            {
                return await _unitOfWork.Reservations.Upsert(entity);
            }
        }

        return null;
    }
}