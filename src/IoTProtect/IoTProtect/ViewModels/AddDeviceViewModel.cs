using System;
using System.Timers;

namespace IoTProtect.ViewModels
{
    public class AddDeviceViewModel : BaseViewModel
    {
        Timer t = new Timer();

        enum AddDeviceStateEnum
        {
            SearchForDevice,
            DeviceSetupBegin,
            DeviceSetupComplete
        }

        public AddDeviceViewModel()
        {
            AddDeviceState = AddDeviceStateEnum.SearchForDevice;
            
            t.Elapsed += T_Elapsed;
            t.AutoReset = true;
            t.Interval = 3000;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (AddDeviceState == AddDeviceStateEnum.SearchForDevice)
            {
                AddDeviceState = AddDeviceStateEnum.DeviceSetupBegin;
            }else if (AddDeviceState == AddDeviceStateEnum.DeviceSetupBegin)
            {
                AddDeviceState = AddDeviceStateEnum.DeviceSetupComplete;
            }else if (AddDeviceState == AddDeviceStateEnum.DeviceSetupComplete)
            {
                AddDeviceState = AddDeviceStateEnum.SearchForDevice;
            }
        }

        AddDeviceStateEnum _AddDeviceState;
        private AddDeviceStateEnum AddDeviceState {
            get
            {
                return _AddDeviceState;
            }
            set
            {
                _AddDeviceState = value;
                UpdateStateProperties();
            }
        }

        bool _atSearchDeviceState;
        public bool atSearchDeviceState
        {
            get
            {
                return _atSearchDeviceState;
            }
            private set
            {
                SetProperty(ref _atSearchDeviceState, value);
            }
        }

        bool _atDeviceSetupBegin;
        public bool atDeviceSetupBegin
        {
            get
            {
                return _atDeviceSetupBegin;
            }
            private set
            {
                SetProperty(ref _atDeviceSetupBegin, value);
            }
        }

        bool _atDeviceSetupCompleteState;
        public bool atDeviceSetupCompleteState
        {
            get
            {
                return _atDeviceSetupCompleteState;
            }
            private set
            {
                SetProperty(ref _atDeviceSetupCompleteState, value);
            }
        }

        bool _DeviceFound;
        public bool DeviceFound
        {
            get
            {
                return _DeviceFound;
            }
            private set
            {
                SetProperty(ref _DeviceFound, value);
            }
        }

        private void UpdateStateProperties()
        {
            switch (AddDeviceState)
            {
                case AddDeviceStateEnum.SearchForDevice:
                    atSearchDeviceState = true;
                    atDeviceSetupBegin = false;
                    atDeviceSetupCompleteState = false;
                    DeviceFound = false;
                    break;

                case AddDeviceStateEnum.DeviceSetupBegin:
                    atSearchDeviceState = false;
                    atDeviceSetupBegin = true;
                    atDeviceSetupCompleteState = false;
                    DeviceFound = true;
                    break;

                case AddDeviceStateEnum.DeviceSetupComplete:
                    atSearchDeviceState = false;
                    atDeviceSetupBegin = false;
                    atDeviceSetupCompleteState = true;
                    DeviceFound = true;
                    break;

                default:
                    break;
            }
        }

    }
}
