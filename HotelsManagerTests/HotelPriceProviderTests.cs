using System;
using FluentAssertions;
using HotelsPriceProvider;
using NUnit.Framework;

namespace HotelPriceProviderTests
{
  public class HotelPriceProviderTests
  {
    private HotelPriceProvider _subject;

    [SetUp]
    public void SetUp()
    {
      _subject = new HotelPriceProvider();
    }

    [Test]
    public void WhenCheckingPriceAndHotelIdIs1_ShouldReturn100()
    {
      _subject.GetPrice(1).Should().Be(100);
    }

    [Test]
    public void WhenCheckingPriceAndHotelIdDoesNotExist_ShouldReturnHotelDoesNotExistError()
    {
      Action checkingPrice = () => { _subject.GetPrice(2); };

      checkingPrice.ShouldThrow<Exception>().WithMessage("Could not find the hotel with the given ID");
    }
  }
}
