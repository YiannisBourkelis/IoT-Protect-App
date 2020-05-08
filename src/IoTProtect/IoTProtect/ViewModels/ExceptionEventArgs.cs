using System;
namespace IoTProtect.ViewModels
{
    public class ExceptionEventArgs : EventArgs
    {
        public ExceptionEventArgs(Exception e)
        {
           exception = e;
        }

        public Exception exception { get; set; }
    }
}
