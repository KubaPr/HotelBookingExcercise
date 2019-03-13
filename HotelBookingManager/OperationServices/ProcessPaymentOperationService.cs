using System;
using PaymentSystem;

namespace HotelBookingManager.OperationServices
{
  public class ProcessPaymentOperationService : IOperationService
  {
    private readonly Payment _paymentSystem;

    public ProcessPaymentOperationService(Payment paymentSystem)
    {
      _paymentSystem = paymentSystem;
    }

    public HotelBookingOperationResult Execute(HotelBookingOperationsData hotelBookingOperationsData)
    {
      try
      {
        var paymentResult = _paymentSystem.ProcessPayment(hotelBookingOperationsData.CreditCardNumber, hotelBookingOperationsData.HotelPrice);

        if (paymentResult.IsSuccessful)
        {
          return new HotelBookingOperationResult();
        }
        return new HotelBookingOperationResult(paymentResult.Message);
      }
      catch (Exception e)
      {
        return new HotelBookingOperationResult(e.Message);
      }
    }
  }
}
