using System;
namespace IoTProtect.Models
{
    public interface IMessaging
    {
        /*
         * TODO: interface default methods only supported on net standard 2.1
         * Οταν παίζει και σε UWP να δοκιμάσω να αλλάξω τα instance properties
         * για να λαμβανω τους τιτλους των messages
        public static string ItemInsertedMessage()
        {
            return "ok";
        }
        */

        public string ItemInsertedMessage { get; }
        public string ItemUpdatedMessage { get; }
        public string ItemDeletedMessage { get; }
    }
}
