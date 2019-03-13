using HotelBookingManager.HotelConfigurations;

namespace HotelBookingManager
{
  public class HotelBookingOperationsManager
  {
    private readonly DummyHotelConfigurationRepository _hotelConfigRepository;

    public HotelBookingOperationsManager(DummyHotelConfigurationRepository hotelConfigRepository)
    {
      _hotelConfigRepository = hotelConfigRepository;
    }

    public HotelBookingProcessResult BookHotel(HotelBookingOperationsData bookingData)
    {
      var processResult = new HotelBookingProcessResult();

      //TODO: Getting configuration from somewhere i.e. config file. For now only works with dummyConfig
      var hotelConfiguration = _hotelConfigRepository.GetDummyConfiguration(bookingData.HotelId);

      foreach (var operation in hotelConfiguration.BookingOperations)
      {
        var operationResult = operation.Service.Execute(bookingData);
        var detailedOperationResult = new HotelBookingDetailedOperationResult(operationResult, operation);
        processResult.DetailedOperationsResults.Add(detailedOperationResult);

        if (!operationResult.IsSuccessful && operation.IsCritical)
        {
          return processResult;
        }
      }
      return processResult;
    }
  }
}