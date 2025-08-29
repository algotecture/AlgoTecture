using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Domain.Enum;
using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.PriceSpecifications;
using Algotecture.Libraries.Reservations.Models;

namespace Algotecture.Libraries.Reservations;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPriceCalculator _priceCalculator;

    public ReservationService(IUnitOfWork unitOfWork, IPriceCalculator priceCalculator)
    {
        _unitOfWork = unitOfWork;
        _priceCalculator = priceCalculator;
    }

    public async Task<IEnumerable<Reservation>> CheckReservation(long spaceId, string subSpaceId, DateTime reservationFrom, DateTime reservationTo)
    {
        var reservation = await _unitOfWork.Reservations.CheckReservation(spaceId, subSpaceId, reservationFrom, reservationTo);

        return reservation;
    }
    
    public async Task<Reservation?> AddReservation(AddReservationModel addReservationModel)
    {
        if (addReservationModel == null) throw new ArgumentNullException(nameof(addReservationModel));

        //simple price for demo
        var targetPriceSpecification =
            (await _unitOfWork.PriceSpecifications.GetBySpaceId(addReservationModel.SpaceId)).FirstOrDefault(x => x.UnitOfTime == UnitOfDateTime.Hour.ToString());
        
        if (targetPriceSpecification == null) throw new ArgumentNullException("No price specification");
        
        var totalPrice = _priceCalculator.CalculateTotalPriceToReservation(addReservationModel.ReservationFromUtc, addReservationModel.ReservationToUtc,
            UnitOfDateTime.Hour, targetPriceSpecification.PricePerTime);
        
        var entity = new Reservation
        {
            ReservationFromUtc = addReservationModel.ReservationFromUtc,
            ReservationToUtc = addReservationModel.ReservationToUtc,
            ReservationStatus = ReservationStatusType.Pending.ToString(),
            TotalPrice = totalPrice,
            SpaceId = addReservationModel.SpaceId,
            SubSpaceId = addReservationModel.SubSpaceId,
            TenantUserId = addReservationModel.TenantUserId,
            ReservationDateTimeUtc = addReservationModel.ReservationDateTimeUtc,
            PriceSpecificationId = targetPriceSpecification.Id,
            Description = addReservationModel.Description,
            ReservationUniqueIdentifier = Guid.NewGuid().ToString()
        };
        
        var reservations = await _unitOfWork.Reservations.CheckReservation(addReservationModel.SpaceId, addReservationModel.SubSpaceId!,
            addReservationModel.ReservationFromUtc, addReservationModel.ReservationToUtc);

        if (reservations.Any())
        {
            throw new InvalidOperationException("Can not add reservation because exist in this period");
        }
        
        var resultReservation = await _unitOfWork.Reservations.Add(entity);
        await _unitOfWork.CompleteAsync();
        return resultReservation;
    }
    
    public async Task<Reservation?> UpdateReservation(UpdateReservationModel updateReservationModel)
    {
        if (updateReservationModel.ReservationId == null) throw new ArgumentNullException(nameof(updateReservationModel.ReservationId));
        if (updateReservationModel == null) throw new ArgumentNullException(nameof(updateReservationModel));
        if (updateReservationModel.ReservationFromUtc == null) throw new ArgumentNullException(nameof(updateReservationModel.ReservationFromUtc));
        if (updateReservationModel.ReservationToUtc == null) throw new ArgumentNullException(nameof(updateReservationModel.ReservationToUtc));
        
        var entity = new Reservation
        {
            Id = updateReservationModel.ReservationId.Value,
            ReservationFromUtc = updateReservationModel.ReservationFromUtc,
            ReservationToUtc = updateReservationModel.ReservationToUtc,
            ReservationStatus = updateReservationModel.ReservationStatus,
            TotalPrice = updateReservationModel.TotalPrice,
            SpaceId = updateReservationModel.SpaceId,
            SubSpaceId = updateReservationModel.SubSpaceId,
            TenantUserId = updateReservationModel.TenantUserId,
            ReservationDateTimeUtc = updateReservationModel.ReservationDateTimeUtc,
            PriceSpecificationId = updateReservationModel.PriceSpecificationId,
            Description = updateReservationModel.Description
        };

        Reservation? resultReservation = null;

        if (updateReservationModel.ReservationId == null) return resultReservation;
        
        var targetReservation = await _unitOfWork.Reservations.GetById(updateReservationModel.ReservationId.Value);
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
        await _unitOfWork.CompleteAsync();

        return updatedReservation;
    }

    public async Task<Reservation?> GetByReservationUniqueIdentifier(string reservationUniqueIdentifier)
    {
        return await _unitOfWork.Reservations.GetByReservationUniqueIdentifier(reservationUniqueIdentifier);
    }
}