using B4.PE3.OmedM.Domain.Models;
using B4.PE3.OmedM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.OmedM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationView : ContentPage
    {
        public LocationView(Location location)
        {
            InitializeComponent();
            BindingContext = new LocationViewModel(location, this.Navigation);
        }
    }
}