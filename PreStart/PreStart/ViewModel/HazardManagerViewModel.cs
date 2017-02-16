﻿using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.Model;
using Prestart.View;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class HazardManagerViewModel : PrestartBaseViewModel
    {
        public HazardManagerViewModel(INavigation nav)
        {
            Navigation = nav;
        }

        ObservableCollection<Hazard> items = new ObservableCollection<Hazard>();
        public ObservableCollection<Hazard> Items
        {
            get { return items; }
            set { SetProperty(ref items, value, "Items"); }
        }

        Hazard selectedItem;
        public Hazard SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value, "SelectedItem");
                if (selectedItem != null)
                {
                    Navigation.PushAsync(new HazardDetail(SelectedItem));
                    SelectedItem = null;
                }
            }
        }

        Command refreshCmd;
        public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var table = await App.CloudService.GetTableAsync<Hazard>();
                var list = await table.ReadAllItemsAsync();
                Items.Clear();
                foreach (var item in list)
                    Items.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command addNewCmd;
        public Command AddNewItemCommand => addNewCmd ?? (addNewCmd = new Command(async () => await ExecuteAddNewItemCommand()));

        async Task ExecuteAddNewItemCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (Settings.SelectedPrestartId == "")
                {
                    await App.Current.MainPage.DisplayAlert("Select a Prestart","You must select a Prestart From the Prestart Log to add Hazards.","Ok");
                    return;
                }
                await Navigation.PushAsync(new HazardForm());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error in AddNewItem: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task RefreshList()
        {
            await ExecuteRefreshCommand();
            MessagingCenter.Subscribe<HazardManagerViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
        }
    }
}