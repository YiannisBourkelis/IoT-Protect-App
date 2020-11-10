using System;
using IoTProtect.Models;
using IoTProtect.Services;
using System.Threading.Tasks;

namespace IoTProtect.ViewModels
{
    public class DevicesListViewModel : BaseListViewModel<Device, DevicesRestService>
    {
        public DevicesListViewModel()
        {
            Title = "Λίστα Ανιχνευτών";

            //Task.Run(async () => await LoadItemsList());
        }
    }
}