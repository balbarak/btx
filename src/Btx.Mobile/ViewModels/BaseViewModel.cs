using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Btx.Mobile.Models;
using Btx.Mobile.Services;
using System.Threading.Tasks;

namespace Btx.Mobile.ViewModels
{
    public class BaseViewModel : INavigation , INotifyPropertyChanged
    {
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

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        #region INavigation implementation

        INavigation _Navigation => (Application.Current?.MainPage as MasterDetailPage).Detail.Navigation;

        public void RemovePage(Page page)
        {
            _Navigation?.RemovePage(page);
        }

        public void InsertPageBefore(Page page, Page before)
        {
            _Navigation?.InsertPageBefore(page, before);
        }

        public Task PushAsync(Page page)
        {
            var task = _Navigation?.PushAsync(page);
            return task;
        }

        public Task<Page> PopAsync()
        {
            var task = _Navigation?.PopAsync();
            return task;
        }

        public Task PopToRootAsync()
        {
            var task = _Navigation?.PopToRootAsync();

            return task;
        }

        public async Task PushModalAsync(Page page)
        {
            var task = _Navigation?.PushModalAsync(page);
            if (task != null)
                await task;
        }

        public async Task<Page> PopModalAsync()
        {
            var task = _Navigation?.PopModalAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PushAsync(Page page, bool animated)
        {
            var task = _Navigation?.PushAsync(page, animated);
            if (task != null)
                await task;
        }

        public async Task<Page> PopAsync(bool animated)
        {
            var task = _Navigation?.PopAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PopToRootAsync(bool animated)
        {
            var task = _Navigation?.PopToRootAsync(animated);
            if (task != null)
                await task;
        }

        public async Task PushModalAsync(Page page, bool animated)
        {
            var task = _Navigation?.PushModalAsync(page, animated);
            if (task != null)
                await task;
        }

        public async Task<Page> PopModalAsync(bool animated)
        {
            var task = _Navigation?.PopModalAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public IReadOnlyList<Page> NavigationStack => _Navigation?.NavigationStack;

        public IReadOnlyList<Page> ModalStack => _Navigation?.ModalStack;
        #endregion  

        public virtual async Task OnAppearing()
        {
            await Task.CompletedTask;
        }

        public virtual async Task OnDisappearing()
        {
            await Task.CompletedTask;
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
