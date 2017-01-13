using System.Diagnostics;
using System.Threading.Tasks;
using Android;
using Android.OS;
using CoffeeFinder.Droid.Services;
using CoffeeFinder.Models;
using CoffeeFinder.Services;
using Java.Lang;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(LocationService))]
namespace CoffeeFinder.Droid.Services
{
    public class LocationService : ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
			//var locator = new Geolocator(Forms.Context) { DesiredAccuracy = 100 };
			//if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
			//{
			//	try
			//	{
			//		var position = await locator.GetPositionAsync(60000);

			//		var result = new GeoCoords
			//		{
			//			Latitude = position.Latitude,
			//			Longitude = position.Longitude
			//		};

			//		return result;
			//	}
			//	catch (Exception ex)
			//	{
					return null;
			//	}
			//}
			//else
			//{
			//	var result = new GeoCoords { Latitude = 38.954577, Longitude = -77.346357 };
			//	return result;
			//}


        }
    }
}