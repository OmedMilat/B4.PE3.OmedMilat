using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using B4.PE3.OmedM.Domain.Models;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using B4.PE3.OmedM.Domain.Services;

namespace B4.PE3.OmedM.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        LocationInMemoryService locationService;
        public MainViewModel()
        {
            locationService = new LocationInMemoryService();
            //Locations = new ObservableCollection<Location>(locationService.GetAll().Result);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                RaisePropertyChanged(nameof(Locations));
            }
        }

        public ICommand GetLocation => new Command(
             async () =>
             {
                 await locationService.AddNewLocation();
                 Locations = new ObservableCollection<Location>(locationService.GetAll().Result);
             });
    }
}
