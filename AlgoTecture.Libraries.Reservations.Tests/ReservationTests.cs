using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Data;
using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Reservations.Models;
using NUnit.Framework;

namespace AlgoTecture.Libraries.Reservations.Tests;

public class ReservationTests
{
    private IUnitOfWork _unitOfWork;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new UnitOfWork(new ApplicationDbContext(Provider.InMemory), null);
    }
    
    [Test]
    public void Throw_InvalidOperationException_If_Reservation_Exist_In_Target_Period()
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

        var addOrUpdateReservationModelDataSeedingTwo = new Reservation()
        {
            Id = 2,
            ReservationFromUtc = DateTime.Parse("2023-03-18 15:00"),
            ReservationToUtc = DateTime.Parse("2023-03-18 18:00"),
            ReservationStatus = "Confirmed",
            TotalPrice = "150",
            SpaceId = 1,
            SubSpaceId = null,
            TenantUserId = 4,
            ReservationDateTimeUtc = DateTime.Parse("2023-03-16 14:00"),
            PriceSpecificationId = 1
        };

        var addOrUpdateReservationModel = new AddOrUpdateReservationModel()
        {
            ReservationFromUtc = DateTime.Parse("2023-03-17 14:00"),
            ReservationToUtc = DateTime.Parse("2023-03-17 17:00"),
            ReservationStatus = "Confirmed",
            TotalPrice = "150",
            SpaceId = 1,
            SubSpaceId = null,
            TenantUserId = 4,
            ReservationDateTimeUtc = DateTime.Parse("2023-03-16 14:00"),
            PriceSpecificationId = 1
        };

        _ = _unitOfWork.Reservations.Add(addOrUpdateReservationModelDataSeedingOne).Result;
        _ = _unitOfWork.Reservations.Add(addOrUpdateReservationModelDataSeedingTwo).Result;
        _unitOfWork.CompleteAsync();
        
        Task Code() => reservationService.AddReservation(addOrUpdateReservationModel);

        Assert.That(Code, Throws.TypeOf<InvalidOperationException>());
    }
}