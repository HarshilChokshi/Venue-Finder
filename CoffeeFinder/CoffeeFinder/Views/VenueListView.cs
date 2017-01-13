using System;
using CoffeeFinder.Models;
using CoffeeFinder.ViewModels;
using Xamarin.Forms;

namespace CoffeeFinder.Views
{
    public class VenueListView : ContentPage
    {
		//public static string selected_website = "";
		//public static string selected_call = "";
		SearchBar Search;
		public static double mile = 1609.34;
		public static double kilo = 1000.00;
		Button milesButton, kiloButton;

		public VenueListView(MainViewModel viewModel)
		{
			NavigationPage.SetHasNavigationBar(this, false);

			BindingContext = viewModel;

			var stack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 20, 0, 0),
				BackgroundColor = Color.Gray
			};

			Search = new SearchBar
			{
				Placeholder = "Restaurant, grocery store, sports shop name etc.",
				FontSize = 11,
				SearchCommand = new Command(() => { })
			};
			stack.Children.Add(Search);

			Search.SearchButtonPressed += async (sender2, e2) =>
			{
				viewModel.searchText = Search.Text.ToString();
				await viewModel.LoadVenues();
			};

			var Logo = new Image
			{
				Source = "logo.png",
				HeightRequest = 30,
				WidthRequest = 200
			};
			stack.Children.Add(Logo);

			var progress = new ActivityIndicator
			{
				IsEnabled = true,
				Color = Color.White
			};

			progress.SetBinding(IsVisibleProperty, "IsBusy");
			progress.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
			stack.Children.Add(progress);

			var validTemplate = new DataTemplate(() =>
			{
				var grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) });
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.2, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) });

				var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
				var addressLabel = new Label();
				var phone_numberLabel = new Label();
				var distanceLabel = new Label();
				var postalCodeLabel = new Label();
				var mobileUrl = new Label();
				var back_image = new Image();
				var websiteButton = new Button();
				websiteButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
				var callButton = new Button();
				callButton.SetBinding(Button.CommandParameterProperty, new Binding("."));

				nameLabel.SetBinding(Label.TextProperty, "Name");
				addressLabel.SetBinding(Label.TextProperty, "Address", stringFormat: "{0:d}");
				//addressLabel.SetBinding(Label.TextProperty, "Website.mobileUrl");
				phone_numberLabel.SetBinding(Label.TextProperty, "Contact.FormattedPhone");
				distanceLabel.SetBinding(Label.TextProperty, "Address.Distance");
				postalCodeLabel.SetBinding(Label.TextProperty, "Address.PostalCode");
				mobileUrl.SetBinding(Label.TextProperty, "Website.mobileUrl");
				back_image.SetBinding(Image.SourceProperty, "Categories[0].Icon", stringFormat: "{0:d}");
				//Categories[0].Icon.Prefix

				nameLabel.TextColor = Color.Black;
				addressLabel.TextColor = Color.Gray;
				addressLabel.FontSize = 11;
				phone_numberLabel.TextColor = Color.Gray;
				phone_numberLabel.FontSize = 11;
				distanceLabel.TextColor = Color.Gray;
				distanceLabel.FontSize = 11;
				postalCodeLabel.TextColor = Color.Gray;
				postalCodeLabel.FontSize = 11;
				back_image.BackgroundColor = Color.Aqua;

				websiteButton.BackgroundColor = Color.Navy;
				websiteButton.Text = "Website";
				websiteButton.TextColor = Color.White;
				websiteButton.FontSize = 10;
				websiteButton.Clicked += OnwebsiteButtonClicked;

				callButton.BackgroundColor = Color.Red;
				callButton.Text = "Call";
				callButton.TextColor = Color.White;
				callButton.FontSize = 10;
				callButton.Clicked += OncallButtonClicked;

				grid.Children.Add(nameLabel, 2, 1);
				grid.Children.Add(addressLabel, 2, 2);
				grid.Children.Add(phone_numberLabel, 2, 3);
				grid.Children.Add(distanceLabel, 1, 4);
				grid.Children.Add(postalCodeLabel, 1, 5);
				grid.Children.Add(callButton, 2, 4);
				grid.Children.Add(websiteButton, 2, 5);
				grid.Children.Add(back_image, 1, 2, 1, 4);

				return new ViewCell
				{
					View = grid
				};
			});

            var listView = new ListView {ItemsSource = viewModel.Venues};
			listView.ItemTemplate = validTemplate;
			listView.RowHeight = 210;
			listView.BackgroundColor = Color.White;
			listView.SeparatorVisibility = SeparatorVisibility.Default;
			listView.ItemSelected += (sender, e) =>
			{
				((ListView)sender).SelectedItem = null;
			};

            stack.Children.Add(listView);

			milesButton = new Button
			{
				Text = "miles",
				TextColor = Color.White,
				FontSize = 9,
				BackgroundColor = Color.Blue,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				BorderColor = Color.Blue,
				BorderWidth = 2,
				BorderRadius = 5,
				HeightRequest = 30,
				WidthRequest = 80
			};
			//milesButton.Clicked += OnMilesButtonClicked;
			milesButton.Clicked += async (sender2, e2) =>
			{
				milesButton.BackgroundColor = Color.Blue;
				kiloButton.BackgroundColor = Color.Transparent;
				GlobalVariables.unitchange = mile;
				await viewModel.LoadVenues();
			};

			kiloButton = new Button
			{
				Text = "kilometers",
				TextColor = Color.White,
				FontSize = 9,
				BackgroundColor = Color.Black,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				BorderColor = Color.Blue,
				BorderWidth = 2,
				BorderRadius = 5,
				HeightRequest = 30,
				WidthRequest = 80
			};

			kiloButton.Clicked += async (sender, e) =>
			{
				milesButton.BackgroundColor = Color.Transparent;
				kiloButton.BackgroundColor = Color.Blue;
				GlobalVariables.unitchange = kilo;
				await viewModel.LoadVenues();
			};

			var bottom_stack = new StackLayout
			{
				Spacing = 0,
				Orientation = StackOrientation.Horizontal,
				BackgroundColor = Color.Black,
				Padding = new Thickness(0, 5, 0, 5),

				Children =
				{ 
					milesButton,kiloButton
				}
			};

			stack.Children.Add(bottom_stack);

			Content = stack;
        }

		void OncallButtonClicked(object sender, EventArgs e)
		{
			var mi = ((Button)sender);
			var m = mi.CommandParameter as Venue;

			if (m.Contact.FormattedPhone == null)
			{
				DisplayAlert("message", "There isn't Phone Number.", "OK");
			}
			else {
				//DependencyService.Get<IPhoneCall>().MakeQuickCall(m.Contact.FormattedPhone);
				Device.OpenUri(new Uri(String.Concat(("tel:"),m.Contact.FormattedPhone)));
			}
		}

		async void OnwebsiteButtonClicked(object sender, EventArgs e)
		{
			var mi = ((Button)sender);
			var m = mi.CommandParameter as Venue;

			if (m.Website == null)
			{
				if (m.Url == null)
				{
					GlobalVariables.selected_website = "";
				}
				else {
					GlobalVariables.selected_website = m.Url.ToString();
				}
			}
			else {
				if (m.Website.mobileUrl == null)
				{
					if (m.Url == null)
					{
						GlobalVariables.selected_website = "";
					}
					else {
						GlobalVariables.selected_website = m.Url.ToString();
					}
				}
				else {
					GlobalVariables.selected_website = m.Website.mobileUrl.ToString();
				}
			}

			if (GlobalVariables.selected_website == "")
			{
				await DisplayAlert("message", "There isn't Website Url.", "OK");
			}
			else {
				//await Navigation.PushAsync(new WebsitePage());
				Device.OpenUri(new Uri(GlobalVariables.selected_website));
			}
		}
	}
}
