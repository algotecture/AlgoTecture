using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;

namespace AlgoTecture.Libraries.Reservations;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Reservation?> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo)
    {
        var reservation = await _unitOfWork.Reservations.CheckReservation(spaceId, subSpaceId, reservationFrom, reservationTo);

        return reservation;
    }
    
    public async Task<Reservation?> AddReservation(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        if (addOrUpdateReservationModel == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel));
        if (addOrUpdateReservationModel.ReservationFrom == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationFrom));
        if (addOrUpdateReservationModel.ReservationTo == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationTo));
        
        var entity = new Reservation
        {
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
        
        var reservation = await _unitOfWork.Reservations.CheckReservation(addOrUpdateReservationModel.SpaceId, addOrUpdateReservationModel.SubSpaceId,
            addOrUpdateReservationModel.ReservationFrom.Value, addOrUpdateReservationModel.ReservationTo.Value);

        if (reservation != null)
        {
            throw new InvalidOperationException("Can not add reservation because exist in this period");
        }
        
        var resultReservation = await _unitOfWork.Reservations.Add(entity);
        return resultReservation;
    }
    
    public async Task<Reservation?> UpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        if (addOrUpdateReservationModel.ReservationId == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationId));
        if (addOrUpdateReservationModel == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel));
        if (addOrUpdateReservationModel.ReservationFrom == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationFrom));
        if (addOrUpdateReservationModel.ReservationTo == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationTo));
        
        var entity = new Reservation
        {
            Id = addOrUpdateReservationModel.ReservationId.Value,
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

        Reservation? resultReservation = null;

        if (addOrUpdateReservationModel.ReservationId == null) return resultReservation;
        
        var targetReservation = await _unitOfWork.Reservations.GetById(addOrUpdateReservationModel.ReservationId.Value);
        if (targetReservation == null) return resultReservation;
            
        resultReservation =  await _unitOfWork.Reservations.Upsert(entity);
        return resultReservation;
    }
}