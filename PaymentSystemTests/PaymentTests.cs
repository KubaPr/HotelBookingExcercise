using FluentAssertions;
using NUnit.Framework;
using PaymentSystem;

namespace PaymentSystemTests
{
  public class PaymentTests
  {
    private Payment _subject;

    [SetUp]
    public void SetUp()
    {
      _subject = new Payment();
    }

    [Test]
    public void WhenCreditCardNumberGivenAndPaymentAmountGiven_ShouldPaymentResultBeSuccessful()
    {
      _subject.ProcessPayment(12345678, 100).IsSuccessful.Should().Be(true);
    }

    [Test]
    public void WhenCreditCardNumberNotGiven_ShouldPaymentResultBeUnsuccessful()
    {
      _subject.ProcessPayment(0, 100).IsSuccessful.Should().Be(false);
    }

    [Test]
    public void WhenPaymentAmountNotGiven_ShouldPaymentResultBeUnsuccessful()
    {
      _subject.ProcessPayment(123, 0).IsSuccessful.Should().Be(false);
    }
  }
}
