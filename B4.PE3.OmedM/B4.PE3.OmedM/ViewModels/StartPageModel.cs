using System.Windows.Input;
using Xamarin.Forms;
using B4.PE3.OmedM.Domain.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;
using B4.PE3.OmedM.Domain.Models;
using System.Runtime.CompilerServices;
using B4.PE3.OmedM.Views;

namespace B4.PE3.OmedM.ViewModels
{
    class StartPageModel : INotifyPropertyChanged
    {      
        LocationInMemoryService locationService;
        public StartPageModel()
        {
            locationService = new LocationInMemoryService();
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

        INavigation navigation;
        public ICommand AddListGps => new Command<Location>(
            async (Location test) =>
            {
               //await locationService.AddNewLocationList();
                await navigation.PushAsync(new MainView());
            });

         
    }
}

