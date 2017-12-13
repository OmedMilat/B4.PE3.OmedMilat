using System.Windows.Input;
using Xamarin.Forms;
using B4.PE3.OmedM.Domain.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;
using B4.PE3.OmedM.Domain.Models;
using System.Runtime.CompilerServices;
using B4.PE3.OmedM.Views;
using System.Xml.Serialization;
using Acr.UserDialogs;
using System.IO;
using System.Xml;
using PCLStorage;


namespace B4.PE3.OmedM.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        LocationInMemoryService locationService;

        public MainViewModel()
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


        public ICommand AddListGps => new Command<ListLocation>(
            async (ListLocation location) =>
            {
                var prompt = new PromptConfig();
                prompt.Message = "Name of the list:";
                prompt.OkText = "Add";
                var result = await UserDialogs.Instance.PromptAsync(prompt);
                if (result.Ok)
                {
                    locationService.Clean();
                    await locationService.AddNewLocationList(result.Text);

                    await navigation.PushAsync(new LocationView(location));
                }
            });

        INavigation navigation;
        public ICommand EditList => new Command<ListLocation>(
            async (ListLocation location) =>
            {
                await navigation.PushAsync(new LocationView(location));
            });

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            locationService = new LocationInMemoryService();
            Listlocations = new ObservableCollection<ListLocation>(locationService.GetAllList().Result);
        }

        public ICommand AppearingCommand => new Command(
       async  () =>
        {
            Listlocations = new ObservableCollection<ListLocation>(locationService.GetAllList().Result);

            if (Listlocations.Count == 0)
            {
                await locationService.LoadLocations();
                Listlocations = new ObservableCollection<ListLocation>(locationService.GetAllList().Result);
            }
        });

    }
}

