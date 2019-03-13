using System;

namespace HotelsPriceProvider
{
  public class HotelPriceProvider
  {
    private readonly Hotel[] _hotels;

    public HotelPriceProvider()
    {
      _hotels = new[]
      {
        new Hotel()
        {
          Id = 1,
        }
      };
    }

    public virtual int GetPrice(int hotelId)
    {
      foreach (var hotel in _hotels)
      {
        if (hotel.Id == hotelId)
        {
          return hotel.GetPrice();
        }
      }
      throw new Exception("Could not find the hotel with the given ID");
    }
  }

  public class Hotel
  {
    public int Id { get; set; }

    public int GetPrice()
    {
      return 100;
    }
  }
}
