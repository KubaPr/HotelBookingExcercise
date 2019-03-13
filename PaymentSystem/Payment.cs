namespace PaymentSystem
{
  public class Payment
  {
    public virtual PaymentResult ProcessPayment(int creditCardNumber, decimal paymentAmount)
    {
      if (creditCardNumber == 0)
      {
        return new PaymentResult
        {
          IsSuccessful = false,
          Message = "Credit card number not given"
        };
      }
      if (paymentAmount == 0)
      {
        return new PaymentResult
        {
          IsSuccessful = false,
          Message = "Payment amount not given"
        };
      }
      return new PaymentResult
      {
        IsSuccessful = true,
        Message = "Payment Successfull"
      };
    }
  }

  public class PaymentResult
  {
    public bool IsSuccessful;
    public string Message;
  }
}
