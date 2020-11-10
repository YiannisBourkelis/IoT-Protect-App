using System;
namespace IoTProtect.Models
{
    public interface ITeam<T> : IItem<T>
    {
        //properties
        public ulong UserID { get; set; }
        public string Name { get; set; }
        public bool PersonalTeam { get; set; }
    }
}
