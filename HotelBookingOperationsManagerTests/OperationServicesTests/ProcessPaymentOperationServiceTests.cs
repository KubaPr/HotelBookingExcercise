using FluentAssertions;
using HotelBookingManager;
using HotelBookingManager.OperationServices;
using NUnit.Framework;
using PaymentSystem;
using Rhino.Mocks;
using System;

namespace HotelBookingOperationsManagerTests.OperationServicesTests
{

  public class ProcessPaymentOperationServiceTests
  {
    private ProcessPaymentOperationService _subject;
    private Payment _stubbedPaymentSystem;
    private HotelBookingOperationsData _dummyData;

    [SetUp]
    public void SetUp()
    {
      _stubbedPaymentSystem = MockRepository.GenerateStub<Payment>();
      _subject = new ProcessPaymentOperationService(_stubbedPaymentSystem);
      _dummyData = new HotelBookingOperationsData(1, 100, DateTime.Today, 12345, "test");
    }

    [Test]
    public void WhenPaymentSystemThrowsException_ShouldReturnUnsuccessfulOperationResult()
    {
      _stubbedPaymentSystem.Stub(
        x => x.ProcessPayment(0, 0)).IgnoreArguments().Throw(new Exception("Dummy exception message"));

      _subject.Execute(_dummyData).IsSuccessful.Should().BeFalse();
    }

    [Test]
    public void WhenPaymentSystemThrowsException_ShouldPassTheErrorMessageAlong()
    {
      const string dummyErrorMessage = "Dummy exception message";
      _stubbedPaymentSystem.Stub(
        x => x.ProcessPayment(0, 0)).IgnoreArguments().Throw(new Exception(dummyErrorMessage));

      _subject.Execute(_dummyData).ErrorMessage.Should().Be(dummyErrorMessage);
    }

    [Test]
    public void WhenPaymentWasSuccessful_ShouldReturnSuccessfulOperationResult()
    {
      _stubbedPaymentSystem.Stub(
        x => x.ProcessPayment(0, 0)).IgnoreArguments().Return(new PaymentResult
      {
        IsSuccessful = true,
        Message = "dummy message"
      });

      _subject.Execute(_dummyData).IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void WhenPaymentWasUnsuccessful_ShouldReturnUnsuccessfulOperationResult()
    {
      _stubbedPaymentSystem.Stub(
        x => x.ProcessPayment(0, 0)).IgnoreArguments().Return(new PaymentResult
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
      _stubbedPaymentSystem.Stub(
        x => x.ProcessPayment(0, 0)).IgnoreArguments().Return(new PaymentResult
        {
          IsSuccessful = false,
          Message = dummyMessage
        });

      _subject.Execute(_dummyData).ErrorMessage.Should().Be(dummyMessage);
    }
  }
}
