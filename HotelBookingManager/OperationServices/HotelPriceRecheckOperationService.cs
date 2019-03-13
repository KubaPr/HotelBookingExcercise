using System;
using HotelsPriceProvider;

namespace HotelBookingManager.OperationServices
{
  public class HotelPriceRecheckOperationService : IOperationService
  {
    private readonly HotelPriceProvider _hotelPriceProvider;

    public HotelPriceRecheckOperationService(HotelPriceProvider hotelPriceProvider)
    {
      _hotelPriceProvider = hotelPriceProvider;
    }

    public HotelBookingOperationResult Execute(HotelBookingOperationsData hotelBookingOperationsData)
    {
      try
      {
        var currentPrice = _hotelPriceProvider.GetPrice(hotelBookingOperationsData.HotelId);
        var userPrice = hotelBookingOperationsData.HotelPrice;

        if (currentPrice != userPrice)
        {
          return new HotelBookingOperationResult("Hotel price has changed during the booking process");
        }
        return new HotelBookingOperationResult();
      }
      catch (Exception e)
      {
        return new HotelBookingOperationResult(e.Message);
      }
    }
  }
}
