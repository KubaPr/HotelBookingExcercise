using HotelBookingManager.HotelConfigurations;

namespace HotelBookingManager
{
  public class HotelBookingDetailedOperationResult
  {
    public HotelBookingOperationResult HotelBookingOperationResult { get; }
    public BookingOperation BookingOperation { get; }

    public HotelBookingDetailedOperationResult(HotelBookingOperationResult hotelBookingOperationResult, BookingOperation bookingOperation)
    {
      HotelBookingOperationResult = hotelBookingOperationResult;
      BookingOperation = bookingOperation;
    }
  }
}
