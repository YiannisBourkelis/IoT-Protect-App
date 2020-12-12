using System;
using IoTProtect.Models;
using IoTProtect.Services;

namespace IoTProtect.ViewModels
{
    public class EnvMonStationMeasurementsViewModel : BaseTeamChildListViewModel<EnvMonStationMeasurement, BaseRestService<EnvMonStationMeasurement>>
    {
        public EnvMonStationMeasurementsViewModel()
        {
            Title = "Λίστα Μετρήσεων";
        }
    }
}
