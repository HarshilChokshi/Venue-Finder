using System;
using CoffeeFinder.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneCall_iOS))]
namespace CoffeeFinder.iOS
{
	public class PhoneCall_iOS : IPhoneCall
	{
		public void MakeQuickCall(string PhoneNumber)
		{
			try
			{
				UIApplication.SharedApplication.OpenUrl(
					new NSUrl("tel:" + PhoneNumber));
			}
			catch (Exception e)
			{

			}
		}
	}
}
