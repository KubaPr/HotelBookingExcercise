using FluentAssertions;
using HotelBookingManager;
using HotelBookingManager.HotelConfigurations;
using NUnit.Framework;


namespace HotelBookingOperationsManagerTests
{
  public class HotelBookingProcessResultTests
  {
    private HotelBookingProcessResult _subject;

    [SetUp]
    public void SetUp()
    {
      _subject = new HotelBookingProcessResult();
    }

    [Test]
    public void WhenOperationWasUnsuccessful_AndOperationIsCritical_ShouldReturnUnsuccessfulBookingProcessResult()
    {
      var detailedHotelBookingOperationResult = new HotelBookingDetailedOperationResult(
        new HotelBookingOperationResult("dummy error message"),
        new BookingOperation(null, "test", true));

      _subject.DetailedOperationsResults.Add(detailedHotelBookingOperationResult);
      _subject.IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenOperationWasUnsuccessful_AndOperationIsNotCritical_ShouldReturnSuccessfulBookingProcessResult()
    {
      var detailedHotelBookingOperationResult = new HotelBookingDetailedOperationResult(
        new HotelBookingOperationResult("dummy error message"),
        new BookingOperation(null, "test", false));

      _subject.DetailedOperationsResults.Add(detailedHotelBookingOperationResult);
      _subject.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void WhenAllOperationsWereSuccessful_ShouldReturnSuccessfulBookingProcessResult()
    {
      var detailedHotelBookingOperationResult = new HotelBookingDetailedOperationResult(
        new HotelBookingOperationResult(),
        new BookingOperation(null, "test", false));

      _subject.DetailedOperationsResults.Add(detailedHotelBookingOperationResult);
      _subject.IsSuccessful.Should().BeTrue();
    }
  }
}
