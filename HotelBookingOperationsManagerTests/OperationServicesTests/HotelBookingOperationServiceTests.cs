using BookingSystem;
using FluentAssertions;
using HotelBookingManager;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using HotelBookingManager.OperationServices;

namespace HotelBookingOperationsManagerTests.OperationServicesTests
{

  public class HotelBookingOperationServiceTests
  {
    private HotelBookingOperationService _subject;
    private HotelBooking _stubbedHotelBookingSystem;
    private HotelBookingOperationsData _dummyData;

    [SetUp]
    public void SetUp()
    {
      _stubbedHotelBookingSystem = MockRepository.GenerateStub<HotelBooking>();
      _subject = new HotelBookingOperationService(_stubbedHotelBookingSystem);
      _dummyData = new HotelBookingOperationsData(1, 100, DateTime.Today, 12345, "test");
    }

    [Test]
    public void WhenHotelBookingSystemThrowsException_ShouldReturnUnsuccessfulOperationResult()
    {
      _stubbedHotelBookingSystem.Stub(
        x => x.BookHotel(_dummyData.HotelId, _dummyData.BookingDate)).Throw(new Exception("Dummy exception message"));

      _subject.Execute(_dummyData).IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenHotelBookingSystemThrowsException_ShouldPassTheErrorMessageAlong()
    {
      const string dummyErrorMessage = "Dummy exception message";
      _stubbedHotelBookingSystem.Stub(
        x => x.BookHotel(_dummyData.HotelId, _dummyData.BookingDate)).Throw(new Exception(dummyErrorMessage));

      _subject.Execute(_dummyData).ErrorMessage.Should().Be(dummyErrorMessage);
    }

    [Test]
    public void WhenHotelBookingWasSuccessful_ShouldReturnSuccessfulOperationResult()
    {
      _stubbedHotelBookingSystem.Stub(
        x => x.BookHotel(_dummyData.HotelId, _dummyData.BookingDate)).Return(new BookingResult
      {
        IsSuccessful = true,
        Message = "dummy message"
      });

      _subject.Execute(_dummyData).IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void WhenHotelBookingWasUnsuccessful_ShouldReturnUnsuccessfulOperationResult()
    {
      _stubbedHotelBookingSystem.Stub(
        x => x.BookHotel(_dummyData.HotelId, _dummyData.BookingDate)).Return(new BookingResult
        {
          IsSuccessful = false,
          Message = "dummy message"
        });

      _subject.Execute(_dummyData).IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenHotelBookingWasUnsuccessful_ShouldPassTheMessageAlongAsErrorMessage()
    {
      const string dummyMessage = "Dummy unsuccessful message";
      _stubbedHotelBookingSystem.Stub(
        x => x.BookHotel(_dummyData.HotelId, _dummyData.BookingDate)).Return(new BookingResult
        {
          IsSuccessful = false,
          Message = dummyMessage
        });

      _subject.Execute(_dummyData).ErrorMessage.Should().Be(dummyMessage);
    }
  }
}
