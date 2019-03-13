using System;

namespace BookingSystem
{
  public class HotelBooking
  {
    public virtual BookingResult BookHotel(int hotelId, DateTime bookingDate)
    {
      if (bookingDate < DateTime.Today)
      {
        return new BookingResult
        {
          IsSuccessful = false,
          Message = "Booking date is before current date"
        };
      }
      return new BookingResult
      {
        IsSuccessful = true,
        Message = "Hotel successfully booked"
      };
    }
  }

  public class BookingResult
  {
    public bool IsSuccessful;
    public string Message;
  }
}
