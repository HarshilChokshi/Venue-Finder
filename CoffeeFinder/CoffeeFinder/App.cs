using CoffeeFinder.Views;
using Xamarin.Forms;

namespace CoffeeFinder
{
	public static class GlobalVariables
	{
		public static double unitchange = 1609.34;
		public static string selected_website = "";
		public static string selected_call = "";
	}

	public class App : Application
    {
		public App ()
		{
			// The root page of your application
			MainPage = new NavigationPage(new MainView());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
    }
}
