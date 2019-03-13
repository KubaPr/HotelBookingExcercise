using System;
using BookingSystem;

namespace HotelBookingManager.OperationServices
{
  public class HotelBookingOperationService : IOperationService
  {
    private readonly HotelBooking _hotelBokingSystem;

    public HotelBookingOperationService(HotelBooking hotelBokingSystem)
    {
      _hotelBokingSystem = hotelBokingSystem;
    }

    public HotelBookingOperationResult Execute(HotelBookingOperationsData hotelBookingOperationsData)
    {
      try
      {
        var bookingResult = _hotelBokingSystem.BookHotel(hotelBookingOperationsData.HotelId,
          hotelBookingOperationsData.BookingDate);

        if (bookingResult.IsSuccessful)
        {
          return new HotelBookingOperationResult();
        }

        return new HotelBookingOperationResult(bookingResult.Message);
      }
      catch (Exception e)
      {
        return new HotelBookingOperationResult(e.Message);
      }
    }
  }
}
