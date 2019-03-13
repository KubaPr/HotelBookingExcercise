using System;
using System.Collections.Generic;
using BookingSystem;
using EmailSender;
using HotelBookingManager.OperationServices;
using HotelsPriceProvider;
using PaymentSystem;

namespace HotelBookingManager.HotelConfigurations
{
  public class DummyHotelConfigurationRepository
  {
    private HotelConfiguration _dummyConfiguration;

    public DummyHotelConfigurationRepository()
    {
      SetDummyConfiguration();
    }

    private void SetDummyConfiguration()
    {
      const int dummyHotelId = 1;
      var dummyOperations = new List<BookingOperation>
      {
        new BookingOperation(new HotelPriceRecheckOperationService(new HotelPriceProvider()), "Price Recheck", true),
        new BookingOperation(new HotelBookingOperationService(new HotelBooking()), "Hotel Booking", true),
        new BookingOperation(new ProcessPaymentOperationService(new Payment()), "Payment", false),
        new BookingOperation(new SendEmailOperationService(new Sender()), "Email notification", false)
      };

      _dummyConfiguration = new HotelConfiguration(dummyHotelId, dummyOperations);
    }

    public virtual HotelConfiguration GetDummyConfiguration(int hotelId)
    {
      if (hotelId == 1) return _dummyConfiguration;
      throw new Exception("Hotel with the given ID is not configured!");
    }
  }
}
