using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Data;
using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Domain.Enum;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.PriceSpecifications;
using AlgoTecture.Libraries.Reservations.Models;
using NUnit.Framework;

namespace AlgoTecture.Libraries.Reservations.Tests;

public class ReservationTests
{
    private IUnitOfWork _unitOfWork = null!;
    private IPriceCalculator _priceCalculator = null!;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new UnitOfWork(new ApplicationDbContext(Provider.InMemory), null!);
        _priceCalculator = new PriceCalculator();
    }
    
    [Test]
    public void Throw_InvalidOperationException_If_Reservation_Exist_In_Target_Period()
    {
        var priceSpecification = new PriceSpecification
        {
            Id = 1,
            SpaceId = 1,
            PricePerTime = "10",
            PriceCurrency = "Usd",
            UnitOfTime = nameof(UnitOfDateTime.Hour)
        };

        var reservationModelDataSeedingOne = new Reservation
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

        var reservationModelDataSeedingTwo = new Reservation
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

        var addReservationModel = new AddReservationModel
        {
            ReservationFromUtc = DateTime.Parse("2023-03-17 14:00"),
            ReservationToUtc = DateTime.Parse("2023-03-17 17:00"),
            SpaceId = 1,
            SubSpaceId = null,
            TenantUserId = 4,
            ReservationDateTimeUtc = DateTime.Parse("2023-03-16 14:00"),
        };

        _ = _unitOfWork.Reservations.Add(reservationModelDataSeedingOne).Result;
        _ = _unitOfWork.Reservations.Add(reservationModelDataSeedingTwo).Result;

        _ = _unitOfWork.PriceSpecifications.Add(priceSpecification).Result;
        _unitOfWork.CompleteAsync();
        
        var reservationService = new ReservationService(_unitOfWork, _priceCalculator);
        
        Task Code() => reservationService.AddReservation(addReservationModel);

        Assert.That(Code, Throws.TypeOf<InvalidOperationException>());
    }
}