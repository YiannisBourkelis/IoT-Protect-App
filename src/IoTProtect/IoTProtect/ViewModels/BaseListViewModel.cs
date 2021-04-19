using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using IoTProtect.Models;
using Xamarin.Forms;
using IoTProtect.Services;

namespace IoTProtect.ViewModels
{
    public class BaseListViewModel<T, TRestService> : BaseViewModel
                        where T : IItem<T>, IMessaging, IRest, new()
                        where TRestService : BaseRestService<T>, new()

    {
        public BaseListViewModel()
        {
            this.RestService = new TRestService();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            DeleteItemCommand = new Command<object>(async (model) => await ExecuteDeleteItemCommand(model));

            RemainingItemsThresholdReachedCommand = new Command(async () => await ExecuteRemainingItemsThresholdReachedCommand());

            ItemsList = new ObservableCollection<T>();

            var tmpItemInstance = new T();
            if (tmpItemInstance.ItemInsertedMessage != null)
            {
                MessagingCenter.Subscribe<object, T>(this, tmpItemInstance.ItemInsertedMessage, async (sender, T) =>
                {
                    await LoadItemsList();
                });
            }

            if (tmpItemInstance.ItemUpdatedMessage != null)
            {
                MessagingCenter.Subscribe<object, T>(this, tmpItemInstance.ItemUpdatedMessage, async (sender, T) =>
                {
                    await LoadItemsList();
                });
            }

            if (tmpItemInstance.ItemDeletedMessage != null)
            {
                MessagingCenter.Subscribe<object, T>(this, tmpItemInstance.ItemDeletedMessage, (sender, T) =>
                {
                    for (int i = ItemsList.Count - 1; i >= 0; i--)
                    {
                        if (ItemsList[i].ID == T.ID)
                        {
                            ItemsList.RemoveAt(i);
                            break;
                        }
                    }
                });
            }
        } // constructor



        //commands
        public Command LoadItemsCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand RemainingItemsThresholdReachedCommand { get; set; }


        ObservableCollection<T> mItemsList;
        public ObservableCollection<T> ItemsList
        {
            get
            {
                return mItemsList;
            }
            set
            {
                SetProperty(ref mItemsList, value);
            }
        }

        bool mIsLoading = false;
        public bool IsLoading
        {
            get
            {
                return mIsLoading;
            }
            set
            {
                SetProperty(ref mIsLoading, value);
            }
        }

        public BaseRestService<T> RestService { get; set; }

        async public Task<bool> LoadItemsList()
        {
            IsLoading = true;
            return await Task.FromResult<bool>(true);
        }

        protected virtual async Task ExecuteLoadItemsCommand()
        {
            try
            {
                var paginator = await RestService.ReadItemsAsync();
                ItemsList.Clear();
                //ελεγχος για null στο .Data property για την περιπτωση που ο χρηστης υπερβει το rate threshold
                paginator.Data?.ForEach(x => { ItemsList.Add(x); });
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool IsLoadingNextPage { get; set; } = false;
        protected virtual async Task ExecuteRemainingItemsThresholdReachedCommand()
        {
            if (IsLoadingNextPage)
            {
                return;
            }

            try
            { 
                IsLoadingNextPage = true;
                await Task.Delay(3000);
                var paginator = await RestService.ReadItemsAsync();
                //ελεγχος για null στο .Data property για την περιπτωση που ο χρηστης υπερβει το rate threshold
                paginator.Data?.ForEach(x => { ItemsList.Add(x); });
            }
            finally
            {
                IsLoadingNextPage = false;
                IsLoading = false;
            }
        }

        async protected virtual Task ExecuteDeleteItemCommand(object obj)
        {
            UserDialogs.Instance.ShowLoading("Γίνεται διαγραφή...");
            T item = (T)obj;
            //var res = await Documents2DataStore.DeleteItemAsync(item);
            var res = await RestService.DeleteItemAsync(item);

            UserDialogs.Instance.HideLoading();

            if (res)
            {
                ToastConfig tconfig = new ToastConfig("Η διαγραφή πραγματοποιήθηκε.")
                {
                    Duration = new TimeSpan(0, 0, 3),
                    MessageTextColor = Color.White,
                    BackgroundColor = Color.ForestGreen
                };
                UserDialogs.Instance.Toast(tconfig);

                MessagingCenter.Send<object, T>(Application.Current, item.ItemDeletedMessage, item);
            }
            else
            {
                ToastConfig tconfig = new ToastConfig("Η διαγραφή δεν πραγματοποιήθηκε.")
                {
                    Duration = new TimeSpan(0, 0, 3),
                    MessageTextColor = Color.White,
                    BackgroundColor = Color.Red
                };
                UserDialogs.Instance.Toast(tconfig);
            }

        }


    }
}
