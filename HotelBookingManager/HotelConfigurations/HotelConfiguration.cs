using System.Collections.Generic;

namespace HotelBookingManager.HotelConfigurations
{
  public class HotelConfiguration
  {
    public int HotelId { get; }
    public List<BookingOperation> BookingOperations { get; }

    public HotelConfiguration(int hotelId, List<BookingOperation> operations)
    {
      HotelId = hotelId;
      BookingOperations = operations;
    }
  }
}