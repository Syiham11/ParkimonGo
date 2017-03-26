
using System;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	public class LoginFragment : BaseFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.LoginFragment, container, false);

			var rootActivity = this.Activity as HomeActivity;

			view.FindViewById<Button>(Resource.Id.ActionLogin).Click += ActionLogin;
			view.FindViewById<Button>(Resource.Id.ActionRegister).Click += (sender, e) => rootActivity.ListItemClicked(3);

			return view;
		}

		async void ActionLogin(object sender, EventArgs e)
		{
			ShowLoadingView("Retriving Daily Deals...");

			var genders = await _apiClient.GetGender();

			HideLoadingView();
			//rootActivity.ListItemClicked(1);
		}
	}
}
