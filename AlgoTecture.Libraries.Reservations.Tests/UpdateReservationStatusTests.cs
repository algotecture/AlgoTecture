using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Data;
using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;
using NUnit.Framework;

namespace AlgoTecture.Libraries.Reservations.Tests;

public class UpdateReservationStatusTests
{
    private IUnitOfWork _unitOfWork;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new UnitOfWork(new ApplicationDbContext(Provider.InMemory), null);
    }

    [Test]
    public void Returned_Confirmed_Status()
    {
        var reservationService = new ReservationService(_unitOfWork);
        
        var addOrUpdateReservationModelDataSeedingOne = new Reservation()
        {
            Id = 1,
            ReservationFromUtc = DateTime.Parse("2023-03-17 15:00"),
            ReservationToUtc = DateTime.Parse("2023-03-17 17:00"),
            ReservationStatus = "Confirmed",
            TotalPrice = "150",
            SpaceId = 1,
            SubSpaceId = null,
            TenantUserId = 1,
            ReservationDateTimeUtc = DateTime.Parse("2023-03-16 14:00"),
            PriceSpecificationId = 1
        };

        var reservation = _unitOfWork.Reservations.Add(addOrUpdateReservationModelDataSeedingOne).Result;
        _unitOfWork.CompleteAsync();

        var updateReservationStatusModel = new UpdateReservationStatusModel()
        {
            ReservationId = 1,
            ReservationStatus = "Contract"
        };

        var updatedReservation = reservationService.UpdateReservationStatus(updateReservationStatusModel.ReservationStatus, updateReservationStatusModel.ReservationId).Result;
        
        Assert.AreEqual("Contract", updatedReservation?.ReservationStatus);
    }
}