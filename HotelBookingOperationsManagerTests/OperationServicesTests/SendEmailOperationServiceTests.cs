using EmailSender;
using FluentAssertions;
using HotelBookingManager;
using HotelBookingManager.OperationServices;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace HotelBookingOperationsManagerTests.OperationServicesTests
{

  public class SendEmailOperationServiceTests
  {
    private SendEmailOperationService _subject;
    private Sender _stubbedSenderServer;
    private HotelBookingOperationsData _dummyData;

    [SetUp]
    public void SetUp()
    {
      _stubbedSenderServer = MockRepository.GenerateStub<Sender>();
      _subject = new SendEmailOperationService(_stubbedSenderServer);
      _dummyData = new HotelBookingOperationsData(1, 100, DateTime.Today, 123456, "test@test.com");
    }

    [Test]
    public void WhenEmailSenderThrowsException_ShouldReturnUnsuccessfulOperationResult()
    {
      _stubbedSenderServer.Stub(
        x => x.SendEmail(null, null)).IgnoreArguments().Throw(new Exception("Dummy exception message"));

      _subject.Execute(_dummyData).IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenEmailSenderThrowsException_ShouldPassTheErrorMessageAlong()
    {
      const string dummyExceptionMessage = "Dummy exception message";
      _stubbedSenderServer.Stub(
        x => x.SendEmail(null, null)).IgnoreArguments().Throw(new Exception(dummyExceptionMessage));

      _subject.Execute(_dummyData).ErrorMessage.Should().Be(dummyExceptionMessage);
    }

    [Test]
    public void WhenEmailSenderDoesNotThrowException_ShouldReturnSuccessfulOperationResult() //ten test kurwa nic chyba nie robi
    {
      _subject.Execute(_dummyData).IsSuccessful.Should().BeTrue();
    }
  }
}
