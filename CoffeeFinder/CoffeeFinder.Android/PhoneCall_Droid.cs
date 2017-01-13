using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Telephony;

using Xamarin.Forms;
using CoffeeFinder.Droid;

[assembly: Dependency(typeof(PhoneCall_Droid))]
namespace CoffeeFinder.Droid
{
	public class PhoneCall_Droid : IPhoneCall
	{
		public void MakeQuickCall(string PhoneNumber)
		{
			try
			{
				var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", PhoneNumber));
				var intent = new Intent(Intent.ActionCall, uri);
				Xamarin.Forms.Forms.Context.StartActivity(intent);
			}
			catch (Exception ex)
			{
				new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("OK", (sender, e) =>
				{
					//User press OK
				})
               .SetMessage(ex.ToString())
               .SetTitle("Android Exception")
               .Show();
			}
		}
	}
}
