using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Enum;
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

    public async Task<IEnumerable<Reservation>> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo)
    {
        var reservation = await _unitOfWork.Reservations.CheckReservation(spaceId, subSpaceId, reservationFrom, reservationTo);

        return reservation;
    }
    
    public async Task<Reservation?> AddReservation(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        if (addOrUpdateReservationModel == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel));
        if (addOrUpdateReservationModel.ReservationFromUtc == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationFromUtc));
        if (addOrUpdateReservationModel.ReservationToUtc == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationToUtc));
        
        var entity = new Reservation
        {
            ReservationFromUtc = addOrUpdateReservationModel.ReservationFromUtc,
            ReservationToUtc = addOrUpdateReservationModel.ReservationToUtc,
            ReservationStatus = addOrUpdateReservationModel.ReservationStatus,
            TotalPrice = addOrUpdateReservationModel.TotalPrice,
            SpaceId = addOrUpdateReservationModel.SpaceId,
            SubSpaceId = addOrUpdateReservationModel.SubSpaceId,
            TenantUserId = addOrUpdateReservationModel.TenantUserId,
            ReservationDateTimeUtc = addOrUpdateReservationModel.ReservationDateTimeUtc,
            PriceSpecificationId = addOrUpdateReservationModel.PriceSpecificationId,
            Description = addOrUpdateReservationModel.Description
        };
        
        var reservations = await _unitOfWork.Reservations.CheckReservation(addOrUpdateReservationModel.SpaceId, addOrUpdateReservationModel.SubSpaceId,
            addOrUpdateReservationModel.ReservationFromUtc.Value, addOrUpdateReservationModel.ReservationToUtc.Value);

        if (reservations.Any())
        {
            throw new InvalidOperationException("Can not add reservation because exist in this period");
        }
        
        var resultReservation = await _unitOfWork.Reservations.Add(entity);
        await _unitOfWork.CompleteAsync();
        return resultReservation;
    }
    
    public async Task<Reservation?> UpdateReservation(AddOrUpdateReservationModel addOrUpdateReservationModel)
    {
        if (addOrUpdateReservationModel.ReservationId == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationId));
        if (addOrUpdateReservationModel == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel));
        if (addOrUpdateReservationModel.ReservationFromUtc == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationFromUtc));
        if (addOrUpdateReservationModel.ReservationToUtc == null) throw new ArgumentNullException(nameof(addOrUpdateReservationModel.ReservationToUtc));
        
        var entity = new Reservation
        {
            Id = addOrUpdateReservationModel.ReservationId.Value,
            ReservationFromUtc = addOrUpdateReservationModel.ReservationFromUtc,
            ReservationToUtc = addOrUpdateReservationModel.ReservationToUtc,
            ReservationStatus = addOrUpdateReservationModel.ReservationStatus,
            TotalPrice = addOrUpdateReservationModel.TotalPrice,
            SpaceId = addOrUpdateReservationModel.SpaceId,
            SubSpaceId = addOrUpdateReservationModel.SubSpaceId,
            TenantUserId = addOrUpdateReservationModel.TenantUserId,
            ReservationDateTimeUtc = addOrUpdateReservationModel.ReservationDateTimeUtc,
            PriceSpecificationId = addOrUpdateReservationModel.PriceSpecificationId,
            Description = addOrUpdateReservationModel.Description
        };

        Reservation? resultReservation = null;

        if (addOrUpdateReservationModel.ReservationId == null) return resultReservation;
        
        var targetReservation = await _unitOfWork.Reservations.GetById(addOrUpdateReservationModel.ReservationId.Value);
        if (targetReservation == null) return resultReservation;
            
        resultReservation =  await _unitOfWork.Reservations.Upsert(entity);
        return resultReservation;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsBySpaceId(long spaceId)
    {
        return await _unitOfWork.Reservations.GetReservationsBySpaceId(spaceId);
    }

    public async Task<Reservation?> UpdateReservationStatus(string reservationStatus, long reservationId)
    {
        if (string.IsNullOrEmpty(reservationStatus)) throw new ArgumentException("Value cannot be null or empty.", nameof(reservationStatus));

        var isValidReservationStatus = Enum.IsDefined(typeof(ReservationStatusType), reservationStatus);

        if (!isValidReservationStatus) throw new InvalidCastException($"{reservationStatus} is not valid");

        var reservation = await _unitOfWork.Reservations.GetById(reservationId);

        if (reservation == null) throw new ArgumentNullException($"Reservation with id {reservationId} not exist");

        reservation.ReservationStatus = reservationStatus;

        var updatedReservation = await _unitOfWork.Reservations.Upsert(reservation);

        return updatedReservation;
    }
}