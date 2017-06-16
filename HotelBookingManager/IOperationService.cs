namespace HotelBookingManager
{
  public interface IOperationService
  {
    HotelBookingOperationResult Execute(HotelBookingOperationsData hotelBookingOperationsData);
  }
}