
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        protected CatalogApiService _catalogApi => DependencyService.Get<CatalogApiService>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        bool _isNotificationVisible;
        public bool IsNotificationVisible
        {
            get => _isNotificationVisible;
            private set => SetProperty(ref _isNotificationVisible, value);
        }

        string _messageNotification;
        public string MessageNotification
        {
            get => _messageNotification;
            private set => SetProperty(ref _messageNotification, value);
        }

        string _imageNotification;
        public string ImageNotification
        {
            get => _imageNotification;
            set => SetProperty(ref _imageNotification, value);
        }
        private EventHandler _eventNotificationSuccess;



        private EventHandler _eventConfirmation;
        bool _isConfirmationVisible;
        public bool IsConfirmationVisible
        {
            get => _isConfirmationVisible;
            set => SetProperty(ref _isConfirmationVisible, value);
        }

        string _messageConfirmation;
        public string MessageConfirmation
        {
            get => _messageConfirmation;
            set => SetProperty(ref _messageConfirmation, value);
        }

        public Command AppearingCommand { get; set; }
        public Command DisappearingCommand { get; set; }
        public Command NotificationCloseCommand { get; set; }

        public Command ConfirmOperationCommand { get; set; }
        public Command CancelOperationCommand { get; set; }

        public BaseViewModel()
        {
            _eventNotificationSuccess = null;
            IsNotificationVisible = false;
            MessageNotification = string.Empty;
            AppearingCommand = new Command(OnAppearing);
            DisappearingCommand = new Command(OnDisappearing);
            NotificationCloseCommand = new Command(NotificationCloseHandler);
            ConfirmOperationCommand = new Command(ConfirmOperationHandler);
            CancelOperationCommand = new Command(CancelOperationHandler);
        }



        protected virtual void OnAppearing(object param)
        {
            //nothing
        }

        protected virtual void OnDisappearing(object param)
        {
            // nothing
        }

        protected void ShowSuccessNotification(string message, EventHandler e)
        {
            ImageNotification = "ok.png";
            MessageNotification = message;
            IsNotificationVisible = true;
            _eventNotificationSuccess += e;
        }


        protected void ShowFailNotification(string message)
        {
            ImageNotification = "error.png";
            MessageNotification = message;
            IsNotificationVisible = true;
        }

        private void NotificationCloseHandler(object obj)
        {
            IsNotificationVisible = false;
            _eventNotificationSuccess?.Invoke(this, EventArgs.Empty);
            _eventNotificationSuccess = null;
        }


        protected void ExecuteIfConfirm(string message, EventHandler e)
        {
            IsConfirmationVisible = true;
            MessageConfirmation = message;
            _eventConfirmation += e;
        }


        private void ConfirmOperationHandler(object obj)
        {
            IsConfirmationVisible = false;
            _eventConfirmation?.Invoke(this, EventArgs.Empty);
            _eventConfirmation = null;
        }

        private void CancelOperationHandler(object obj)
        {
            IsConfirmationVisible = false;
            _eventConfirmation = null;
        }


        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
