using System;

namespace HotelBookingManager
{
  public class HotelBookingOperationsData
  {
    public int HotelId { get; }
    public decimal HotelPrice { get; }
    public DateTime BookingDate { get; }
    public int CreditCardNumber { get; }
    public string EmailAddress { get; }

    public HotelBookingOperationsData(int hotelId, decimal hotelPrice, DateTime bookingDate, int creditCardNumber, string emailAddress)
    {
      HotelId = hotelId;
      HotelPrice = hotelPrice;
      BookingDate = bookingDate;
      CreditCardNumber = creditCardNumber;
      EmailAddress = emailAddress;
    }
  }
}