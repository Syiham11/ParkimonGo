
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	public class RegisterFragment : BaseFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.RegisterFragment, container, false);

			var rootActivity = this.Activity as HomeActivity;

			Spinner spinner = view.FindViewById<Spinner>(Resource.Id.selectTitle);

			String[] titles = { "Select your title", "KKR", "CSK", "RR", "KXIP", "RR", "MI" };

			var adapter = new ArrayAdapter(this.Activity, Resource.Drawable.spinner_selector, titles);

			spinner.Adapter = adapter;
			adapter.SetDropDownViewResource(Resource.Drawable.spinner_selector);

			var aaa = spinner.Prompt;

			view.FindViewById<Button>(Resource.Id.ActionRegister).Click += (sender, e) => rootActivity.ListItemClicked(1);

			return view;
		}
	}
}
