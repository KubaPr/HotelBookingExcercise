namespace HotelBookingManager.HotelConfigurations
{
  public class BookingOperation
  {
    public BookingOperation(IOperationService service, string name, bool isCritical)
    {
      Service = service;
      Name = name;
      IsCritical = isCritical;
    }

    public IOperationService Service { get; }
    public string Name { get; }
    public bool IsCritical { get; }
  }
}
