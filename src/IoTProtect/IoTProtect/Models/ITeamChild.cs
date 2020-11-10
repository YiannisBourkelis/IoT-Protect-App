using System;
namespace IoTProtect.Models
{
    public interface ITeamChild<T> : IItem<T>
    {
        public ulong DeviceID { get; set; }
    }
}
