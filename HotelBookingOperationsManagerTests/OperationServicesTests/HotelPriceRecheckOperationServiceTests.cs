using System;
using FluentAssertions;
using HotelBookingManager;
using NUnit.Framework;
using HotelBookingManager.OperationServices;
using HotelsPriceProvider;
using Rhino.Mocks;

namespace HotelBookingOperationsManagerTests.OperationServicesTests
{

  public class HotelPriceRecheckOperationServiceTests
  {
    private HotelPriceRecheckOperationService _subject;
    private HotelPriceProvider _stubbedHotelPriceProvider;
    private HotelBookingOperationsData _dummyData;

    [SetUp]
    public void SetUp()
    {
      _stubbedHotelPriceProvider = MockRepository.GenerateStub<HotelPriceProvider>();
      _subject = new HotelPriceRecheckOperationService(_stubbedHotelPriceProvider);

      _dummyData = new HotelBookingOperationsData(1, 100, DateTime.Today, 123456, "test@test.com");
    }

    [Test]
    public void WhenHotelPriceProviderThrowsException_ShouldReturnUnsuccessfulOperationResult()
    {
      _stubbedHotelPriceProvider.Stub(
        x => x.GetPrice(0)).IgnoreArguments().Throw(new Exception("Dummy exception message"));

      _subject.Execute(_dummyData).IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenHotelPriceProviderThrowsException_ShouldPassTheErrorMessageAlong()
    {
      const string dummyErrorMessage = "Dummy exception message";
      _stubbedHotelPriceProvider.Stub(
        x => x.GetPrice(0)).IgnoreArguments().Throw(new Exception(dummyErrorMessage));

      _subject.Execute(_dummyData).ErrorMessage.Should().Be(dummyErrorMessage);
    }

    [Test]
    public void WhenHotelPriceProviderReturnsHotelPrice_AndReturnedPriceEqualsUserPrice_ShouldReturnSuccessfulOperationResult()
    {
      _stubbedHotelPriceProvider.Stub(x => x.GetPrice(0)).IgnoreArguments().Return(100);

      _subject.Execute(_dummyData).IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void WhenHotelPriceProviderReturnsHotelPrice_AndReturnedPriceIsNotEqualToUserPrice_ShouldReturnuUnsuccessfulOperationResult()
    {
      _stubbedHotelPriceProvider.Stub(x => x.GetPrice(0)).IgnoreArguments().Return(99);

      _subject.Execute(_dummyData).IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenHotelPriceProviderReturnsHotelPrice_AndReturnedPriceIsNotEqualToUserPrice_ShouldReturnTheErrorMessage()
    {
      _stubbedHotelPriceProvider.Stub(x => x.GetPrice(0)).IgnoreArguments().Return(99);

      _subject.Execute(_dummyData).ErrorMessage.Should().Be("Hotel price has changed during the booking process");
    }
  }
}
