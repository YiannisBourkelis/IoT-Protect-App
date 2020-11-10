using System;
using System.Threading.Tasks;
using IoTProtect.Models;
using IoTProtect.Services;

namespace IoTProtect.ViewModels
{
    public class BaseTeamChildListViewModel<T, TRestService> : BaseListViewModel<T, TRestService>
                                        where T : ITeamChild<T>, IMessaging, IRest, new()
                                        where TRestService : BaseTeamRestService<T>, new()
    { 


        public BaseTeamChildListViewModel()
        {
        }

        private Device mDevice;
        public Device Device {
            get
            {
                return mDevice;
            }
            set
            {
                SetProperty(ref mDevice, value);
            }
        }

        async protected override Task ExecuteLoadItemsCommand()
        {
            try
            {
                var tChild = new T { DeviceID = this.Device.ID };
                var paginator = await RestService.ReadItemsAsync(tChild);
                ItemsList.Clear();
                //ελεγχος για null στο .Data property για την περιπτωση που ο χρηστης υπερβει το rate threshold
                paginator.Data?.ForEach(x => { ItemsList.Add(x); });
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
