using B4.PE3.OmedM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.OmedM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}