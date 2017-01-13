using CoffeeFinder.ViewModels;
using Xamarin.Forms;

namespace CoffeeFinder.Views
{
	public class MainView : ContentPage
    {
        private MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }

        public MainView()
        {
            BindingContext = new MainViewModel();

    		Navigation.PushAsync(new VenueListView(_vm));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm == null || _vm.IsBusy || _vm.Venues.Count > 0)
                return;
            
            _vm.RefreshCommand.Execute(null);
        }
    }
}
