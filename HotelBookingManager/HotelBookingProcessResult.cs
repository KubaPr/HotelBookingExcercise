using System.Collections.Generic;
using System.Linq;

namespace HotelBookingManager
{
  public class HotelBookingProcessResult
  {
    public bool IsSuccessful
    {
      get
      {
        return DetailedOperationsResults.All(
          operation => !operation.BookingOperation.IsCritical || 
          operation.HotelBookingOperationResult.IsSuccessful);
      }
    }
    public string ReservationId => IsSuccessful ? "DummyReservationID" : "";
    public List<HotelBookingDetailedOperationResult> DetailedOperationsResults { get; set; } = new List<HotelBookingDetailedOperationResult>();
  }
}
