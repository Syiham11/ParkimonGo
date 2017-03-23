using Android.App;
using Android.OS;
using Android.Views;

//using AndroidHUD;

using FragmentActivity = Android.Support.V4.App.FragmentActivity;

namespace ParkimonGo.Droid
{
	public class BaseActivity : FragmentActivity
	{
		AlertDialog.Builder alert;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Window.RequestFeature(WindowFeatures.NoTitle);
		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		public void ShowLoadingView(string title)
		{
			//AndHUD.Shared.Show(this, title, -1, MaskType.Black);
		}

		public void HideLoadingView()
		{
			//AndHUD.Shared.Dismiss(this);
		}

		public void ShowMessageBox(string title, string message)
		{
			alert = new AlertDialog.Builder(this);
			alert.SetTitle(title);
			alert.SetMessage(message);
			alert.SetPositiveButton("OK", (senderAlert, args) =>
			{
			});
			RunOnUiThread(() =>
			{
				alert.Show();
			});
		}

		//protected void OnBack()
		//{
		//	base.OnBackPressed();
		//	OverridePendingTransition(Resource.Animation.fromRight, Resource.Animation.toLeft);
		//}

	}
}

