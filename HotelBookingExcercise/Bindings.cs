using HotelBookingManager;
using Ninject.Modules;

namespace HotelBookingExcercise
{
  public class Bindings : NinjectModule
  {
    public override void Load()
    {
      Bind<HotelBookingOperationsManager>().To<HotelBookingOperationsManager>();
    }
  }
}
