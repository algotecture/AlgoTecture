﻿using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Data.Persistence.Data;
using Algotecture.Data.Persistence.Ef;
using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.PriceSpecifications;
using Algotecture.Libraries.Reservations.Models;
using NUnit.Framework;

namespace Algotecture.Libraries.Reservations.Tests;

public class UpdateReservationStatusTests
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
    public void Returned_Confirmed_Status()
    {
        var reservationService = new ReservationService(_unitOfWork, _priceCalculator);
        
        var addOrUpdateReservationModelDataSeedingOne = new Reservation
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
        
        Assert.Equals("Contract", updatedReservation?.ReservationStatus!);
    }
}