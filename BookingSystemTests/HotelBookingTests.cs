using System;
using BookingSystem;
using FluentAssertions;
using NUnit.Framework;

namespace BookingSystemTests
{

  public class HotelBookingTests
  {
    private HotelBooking _subject;

    [SetUp]
    public void SetUp()
    {
      _subject = new HotelBooking();
    }

    [Test]
    public void WhenBookingHotelAndHotelAvailable_ShouldBookingResultBeSuccesfull()
    {
      _subject.BookHotel(1, DateTime.Today).IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void WhenBookingDateBeforeToday_ShouldBookingResultBeUnsuccesfull()
    {
      var today = DateTime.Now;
      _subject.BookHotel(1, today.AddDays(-1)).IsSuccessful.Should().BeFalse();
    }
  }
}

