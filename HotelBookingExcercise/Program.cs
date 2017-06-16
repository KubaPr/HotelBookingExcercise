using System;
using System.Reflection;
using HotelBookingManager;
using Ninject;

namespace HotelBookingExcercise
{
  //TODO: Extract this logic to Console handling class! For now everything happens here
  //TODO: Refactoring - number of shits given while writing this: 0. Only for "demo" purposes
  //TODO: Get the data from the console, for now only hardcoded data can be used

  class Program
  {
    static void Main(string[] args)
    {
      var kernel = new StandardKernel();
      kernel.Load(Assembly.GetExecutingAssembly());

      var hotelBookingOperationsManager = kernel.Get<HotelBookingOperationsManager>();

      HotelBookingProcessResult bookingProcessResult = null;

      var testBookingData = new HotelBookingOperationsData(1, 100, DateTime.Today, 1234, "");

      try
      {
        bookingProcessResult = hotelBookingOperationsManager.BookHotel(testBookingData);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

      if (bookingProcessResult != null)
      {
        foreach (var operationResult in bookingProcessResult.DetailedOperationsResults)
        {
          var result = "";
          result += operationResult.BookingOperation.Name;

          if (operationResult.HotelBookingOperationResult.IsSuccessful)
          {
            result += " operation successful!";
          }
          else
          {
            result += " operation unsuccessful: ";
          }

          result += operationResult.HotelBookingOperationResult.ErrorMessage;

          Console.WriteLine(result);
        }
      }

      Console.WriteLine("----------------------------------------------------------------------------------------------");

      if (bookingProcessResult != null && bookingProcessResult.IsSuccessful)
      {
        Console.WriteLine("Booking process successful! Your Reservation ID is " + bookingProcessResult.ReservationId);
      }
      else
      {
        Console.WriteLine("Booking process unsuccessful");
      }
      Console.ReadLine();
    }
  }
}
