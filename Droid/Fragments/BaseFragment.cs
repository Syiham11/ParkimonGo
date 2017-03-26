
using Android.Graphics;
using Android.OS;
using Android.Views;
using AndroidHUD;
using Fragment = Android.Support.V4.App.Fragment;

namespace ParkimonGo.Droid
{
	public class BaseFragment : Fragment
	{
		public ApiClient _apiClient = new ApiClient();

		public Color _cOrange = Color.ParseColor("#FF6e2b");
		public Color _cGray = Color.ParseColor("#404040");

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public void ShowLoadingView(string title)
		{
			Activity.RunOnUiThread(() =>
			{
				AndHUD.Shared.Show(Activity, title, -1, MaskType.Black);
			});
		}

		public void HideLoadingView()
		{
			Activity.RunOnUiThread(() =>
			{
				AndHUD.Shared.Dismiss(Activity);
			});
		}
	}
}
