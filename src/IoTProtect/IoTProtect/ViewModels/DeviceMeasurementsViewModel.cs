using System;
using IoTProtect.Models;
using IoTProtect.Services;

namespace IoTProtect.ViewModels
{
    public class DeviceMeasurementsViewModel : BaseTeamChildListViewModel<SmokeDetectorMeasurement, BaseRestService<SmokeDetectorMeasurement>>
    {
        public DeviceMeasurementsViewModel()
        {
            Title = "Λίστα Μετρήσεων";
        }
    }
}
