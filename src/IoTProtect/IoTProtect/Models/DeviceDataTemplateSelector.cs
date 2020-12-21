using System;
using Xamarin.Forms;

namespace IoTProtect.Models
{

    public class DeviceDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Smoke { get; set; }
        public DataTemplate EnvMonSation { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var dev_type = (item as Device).Type;
            switch (dev_type)
            {
                case DeviceModelsEnum.SmokePhotoelectricFlameTemp_v1:
                    return Smoke;
                    //break;
                case DeviceModelsEnum.EnvironmentalMonStation_v1:
                    return EnvMonSation;
                    //break;
                default:
                    return null;
                    //break;
            }
        }
            
    }//class DeviceDataTemplateSelector

}//namespace
