using System;
namespace IoTProtect.Models
{
    public interface IItem<T>
    {
        public ulong ID { get; set; }

        //methods
        public bool HaveSamePropertyValues(T item);
    }
}
