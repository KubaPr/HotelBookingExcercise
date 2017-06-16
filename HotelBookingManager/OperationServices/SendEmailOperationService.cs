using System;
using EmailSender;

namespace HotelBookingManager.OperationServices
{
  //Assumption: the e-mail is sent no matter what at the moment
  //TODO: e-mail to be sent with the confirmation of the process
  public class SendEmailOperationService : IOperationService
  {
    private readonly Sender _emailSender;

    public SendEmailOperationService(Sender emailSender)
    {
      _emailSender = emailSender;
    }

    public HotelBookingOperationResult Execute(HotelBookingOperationsData hotelBookingOperationsData)
    {
      try
      {
        _emailSender.SendEmail(hotelBookingOperationsData.EmailAddress, "dupa");
      }
      catch (Exception e)
      {
        return new HotelBookingOperationResult(e.Message);
      }
      return new HotelBookingOperationResult();
    }
  }
}
