using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoffeeFinder.Models;
using CoffeeFinder.Services;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace CoffeeFinder.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IFoursquareService _foursquare;
        private readonly ILocationService _location;

		public string searchText { get; set; }

        private ObservableCollection<Venue> _venues;
        public ObservableCollection<Venue> Venues
        {
            get { return _venues; }
            set
            {
                _venues = value;
                OnPropertyChanged();
            }
        }

        private GeoCoords _currentLocation;
        public GeoCoords CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Venues = new ObservableCollection<Venue>();

            // TODO: constructor injection
            _foursquare = new FoursquareService();
            _location = DependencyService.Get<ILocationService>();
        }

        private Command _refreshCommand;
        public Command RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        private async Task ExecuteRefreshCommand()
        {
            if (IsBusy) return;

            //CurrentLocation  = new GeoCoords { Latitude = 38.954577, Longitude = -77.346357 }; // Reston VA
            //CurrentLocation = await _location.GetGeoCoordinatesAsync();
			CurrentLocation = await GetCoordinator();
            MessagingCenter.Send(this, "LocationSet");

            await LoadVenues();
        }

		public async Task LoadVenues()
        {
            if (IsBusy) return;
            IsBusy = true;

            Venues.Clear();

            try
            {
				if (searchText == null)
				{
					Venues.Clear();
				}
				else {
	                var response = await _foursquare.GetVenues(searchText, CurrentLocation);
					foreach (var v in response.Venues)
						Venues.Add(v);
				}
                
                // Publish message that Venues collection has been fully loaded
                MessagingCenter.Send(this, "VenuesLoaded");
            }
			catch (System.Exception exception)
			{
				Venues.Clear();
			}
            finally
            {
                IsBusy = false;
            }
        }

		public async Task<GeoCoords> GetCoordinator()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

			if (position == null)
			{
				return null;
			}

			var result = new GeoCoords
			{
				Latitude = position.Latitude,
				Longitude = position.Longitude
			};

			return result;
		}
    }
}
