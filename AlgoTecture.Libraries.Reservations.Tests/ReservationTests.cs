using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Data.Persistence.Data;
using AlgoTecture.Data.Persistence.Ef;
using AlgoTecture.Libraries.Reservations.Models;
using NUnit.Framework;

namespace AlgoTecture.Libraries.Reservations.Tests;

public class ReservationTests
{
    private IUnitOfWork _unitOfWork;

    [SetUp]
    public void SetUp()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var connectionString = $"DataSource={currentDirectory}/app.db;Cache=Shared";
        _unitOfWork = new UnitOfWork(new ApplicationDbContext(connectionString), null);
    } 
    [Test]
    public void Assert_That_Will_be_Returned_Null_If_In_Target_Time_Reservation_Exist()
    {
        var reservationService = new ReservationService(_unitOfWork);

        var addOrUpdateReservationModel = new AddOrUpdateReservationModel()
        {
            ReservationFrom = DateTime.Parse("2023-03-17 14:00"),
            ReservationTo = DateTime.Parse("2023-03-17 17:00"),
            ReservationStatus = "Confirmed",
            TotalPrice = "150",
            SpaceId = 1,
            SubSpaceId = null,
            TenantUserId = 4,
            ReservationDateTime = DateTime.Parse("2023-03-16 14:00"),
            PriceSpecificationId = 1
        };

        var result = reservationService.AddOrUpdateReservation(addOrUpdateReservationModel).Result;
        
        Assert.AreEqual(null, result);
    }
}