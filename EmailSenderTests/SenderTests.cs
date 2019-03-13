using System;
using EmailSender;
using FluentAssertions;
using NUnit.Framework;

namespace EmailSenderTests
{
  public class SenderTests
  {
    private Sender _subject;

    [SetUp]
    public void SetUp()
    {
      _subject = new Sender();
    }

    [Test]
    public void WhenEmailAddressNotGiven_ShouldReturnNoEmailAddressError()
    {
      Action sendingEmail = () => { _subject.SendEmail(null, "Dummy message"); };

      sendingEmail.ShouldThrow<Exception>().WithMessage("No e-mail address given!");
    }

    [Test]
    public void WhenMessageIsNotGiven_ShouldReturnNoMessageGivenError()
    {
      Action sendingEmail = () => { _subject.SendEmail("dummyMail", ""); };

      sendingEmail.ShouldThrow<Exception>().WithMessage("Message not given!");
    }
  }
}
