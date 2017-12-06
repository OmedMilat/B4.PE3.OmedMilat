using System.Windows.Input;
using Xamarin.Forms;
using B4.PE3.OmedM.Domain.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;
using B4.PE3.OmedM.Domain.Models;
using System.Runtime.CompilerServices;
using B4.PE3.OmedM.Views;
using System;
using Acr.UserDialogs;

namespace B4.PE3.OmedM.ViewModels
{
    class StartPageModel : INotifyPropertyChanged
    {
        LocationInMemoryService locationService;
        
        public StartPageModel()
        {
            locationService = new LocationInMemoryService();
            Listlocations = new ObservableCollection<ListLocation>(locationService.GetAllList().Result);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        ObservableCollection<ListLocation> listlocations;
        public ObservableCollection<ListLocation> Listlocations
        {
            get { return listlocations; }
            set
            {
                listlocations = value;
                RaisePropertyChanged();
            }
        }

        
        public ICommand AddListGps => new Command<Location>(
            async (Location location) =>
            {
                await locationService.AddNewLocationList();
                await navigation.PushAsync(new MainView(location));
            });

        INavigation navigation;
        public ICommand EditList => new Command<Location>(
            async (Location location) =>
            {
                await navigation.PushAsync(new MainView(location));
            });

        public StartPageModel(INavigation navigation)
        {
            this.navigation = navigation;

            locationService = new LocationInMemoryService();
            Listlocations = new ObservableCollection<ListLocation>(locationService.GetAllList().Result);
        }

        public ICommand AppearingCommand => new Command(
         () =>
        {
            Listlocations = new ObservableCollection<ListLocation>(locationService.GetAllList().Result);
        });

    }
}

