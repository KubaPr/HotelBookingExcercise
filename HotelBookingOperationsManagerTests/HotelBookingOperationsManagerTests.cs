
using FluentAssertions;
using HotelBookingManager;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using HotelBookingManager.HotelConfigurations;


namespace HotelBookingOperationsManagerTests
{
  public class HotelBookingOperationsManagerTests
  {
    private HotelBookingOperationsManager _subject;
    private HotelBookingOperationsData _dummyData;
    private DummyHotelConfigurationRepository _dummyConfigRepository;

    private IOperationService _dummyCriticalOperationService;
    private IOperationService _dummyNonCriticalOperationService;

    private HotelConfiguration _dummyConfiguration;

    [SetUp]
    public void SetUp()
    {
      _dummyCriticalOperationService = MockRepository.GenerateStub<IOperationService>();
      _dummyNonCriticalOperationService = MockRepository.GenerateStub<IOperationService>();

      _dummyConfiguration = new HotelConfiguration(1, new List<BookingOperation>
      {
        new BookingOperation(_dummyCriticalOperationService, "Critical Operation", true),
        new BookingOperation(_dummyNonCriticalOperationService, "Non-critical Operation", false)
      });

      _dummyConfigRepository = MockRepository.GenerateStub<DummyHotelConfigurationRepository>();
      _dummyConfigRepository.Stub(x => x.GetDummyConfiguration(0)).IgnoreArguments().Return(_dummyConfiguration);

      _dummyData = new HotelBookingOperationsData(1, 100, DateTime.Today, 12345, "test");

      _subject = new HotelBookingOperationsManager(_dummyConfigRepository);
    }

    [Test]
    public void ShouldAddDetailedOperationResultToTheList()
    {
      const string dummyErrorMessage = "dummy error message";
      _dummyCriticalOperationService.Stub(
        x => x.Execute(null)).IgnoreArguments().Return(new HotelBookingOperationResult());
      _dummyNonCriticalOperationService.Stub(
        x => x.Execute(null)).IgnoreArguments().Return(new HotelBookingOperationResult(dummyErrorMessage));

      var dummyResult = _dummyNonCriticalOperationService.Execute(_dummyData);
      var dummyDetailedOperationResult = new HotelBookingDetailedOperationResult(
        dummyResult,
        _dummyConfiguration.BookingOperations[1]);

      //this bastard uses equals, not comparing by a reference so can't use it:
      //_subject.BookHotel(_dummyData, _dummyConfiguration)
      //  .DetailedOperationsResults.Should()
      //  .Contain(dummyDetailedOperationResult);

      _subject.BookHotel(_dummyData)
        .DetailedOperationsResults[1].ShouldBeEquivalentTo(dummyDetailedOperationResult); //worst assert ever but no idea how to do this better
    }

    [Test]
    public void WhenOperationWasUnsuccessful_AndOperationIsCritical_ShouldReturnUnsuccessfulProcessResult()
    {
      _dummyCriticalOperationService.Stub(
        x => x.Execute(null)).IgnoreArguments().Return(new HotelBookingOperationResult("dummy error message"));

      _subject.BookHotel(_dummyData).IsSuccessful.Should().BeFalse();
    }
  }
}
