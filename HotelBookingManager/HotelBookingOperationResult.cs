namespace HotelBookingManager
{
  public class HotelBookingOperationResult
  {
    public bool IsSuccessful { get; }
    public string ErrorMessage { get; }

    public HotelBookingOperationResult(string errorMessage)
    {
      ErrorMessage = errorMessage;
      IsSuccessful = false;
    }

    public HotelBookingOperationResult()
    {
      IsSuccessful = true;
    }
  }
}