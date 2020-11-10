using System;
using Acr.UserDialogs;
using System.Drawing;
using FluentValidation.Results;

namespace IoTProtect.Helpers
{
    public static class Dialog
    {
        public static void DisplaySuccessToast(string message)
        {
            ToastConfig tconfig = new ToastConfig(message)
            {
                Duration = new TimeSpan(0, 0, 3),
                MessageTextColor = Color.White,
                BackgroundColor = Color.ForestGreen
            };
            UserDialogs.Instance.Toast(tconfig);
        }

        public static void DisplayErrorToast(string message)
        {
            ToastConfig tconfig = new ToastConfig(message)
            {
                Duration = new TimeSpan(0, 0, 3),
                MessageTextColor = Color.White,
                BackgroundColor = Color.Red
            };
            UserDialogs.Instance.Toast(tconfig);
        }

        public static void DisplayValidationErrorToast(ValidationResult validationResult, string Title)
        {
            //TODO: να δώ πως μπορεί να απεικονίσει περισσότερες από 2 γραμμές
            // γιατι τώρα εμφανίζεται το μύνημα truncated αν υπάρχουν 2 validation errors ή περισσότερα
            foreach (var err in validationResult.Errors)
            {
                Title += "\n";
                Title += "► " + err.ErrorMessage;
            }

            ToastConfig tconfig = new ToastConfig(Title)
            {
                Duration = new TimeSpan(0, 0, 3),
                MessageTextColor = Color.White,
                BackgroundColor = Color.Red,
                Position = ToastPosition.Top
            };
            UserDialogs.Instance.Toast(tconfig);
        }

    }
}

