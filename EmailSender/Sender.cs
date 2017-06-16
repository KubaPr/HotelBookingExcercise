using System;

namespace EmailSender
{
  public class Sender
  {
    public virtual void SendEmail(string recipientEmailAddress, string message)
    {
      if (string.IsNullOrEmpty(recipientEmailAddress))
      {
        throw new Exception("No e-mail address given!");
      }
      if (string.IsNullOrEmpty(message))
      {
        throw new Exception("Message not given!");
      }
    }
  }
}
