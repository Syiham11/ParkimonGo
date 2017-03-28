
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
using GalaSoft.MvvmLight.Helpers;

namespace ParkimonGo.Droid
{
	public class RegisterFragment : BaseFragment
	{
		RequestRegister _requestRegister = new RequestRegister();

		HomeActivity _rootVC;
		View _view;

		String[] _titles = { "Select your title", "KKR", "CSK", "RR", "KXIP", "RR", "MI" };
		String[] _suffixes = { "Select your suffix", "KKR", "CSK", "RR", "KXIP", "RR", "MI" };
		String[] _months = { "Month", "Jan", "Feb", "Mar", "Apr", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
		String[] _days = { "Day", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
		String[] _Year = { "Year", "1990", "1991", "...", "2017" };

		TextView _txtFirstname, _txtMiddleName, _txtLastName, _txtEmail, _txtPassword, _txtPFirstName;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_view = inflater.Inflate(Resource.Layout.RegisterFragment, container, false);

			_rootVC = this.Activity as HomeActivity;

			InitUISettings();

			return _view;
		}

		void InitUISettings()
		{
			Spinner spTitle = _view.FindViewById<Spinner>(Resource.Id.spTitle);
			Spinner spSuffix = _view.FindViewById<Spinner>(Resource.Id.spSuffix);
			Spinner spMonth = _view.FindViewById<Spinner>(Resource.Id.spMonth);
			Spinner spDay = _view.FindViewById<Spinner>(Resource.Id.spDay);
			Spinner spYear = _view.FindViewById<Spinner>(Resource.Id.spYear);

			var adpterTitle = new ArrayAdapter(this.Activity, Resource.Drawable.spinner_selector, _titles);
			var adpterSuffix = new ArrayAdapter(this.Activity, Resource.Drawable.spinner_selector, _suffixes);
			var adpterMonth = new ArrayAdapter(this.Activity, Resource.Drawable.spinner_selector, _months);
			var adpterDay = new ArrayAdapter(this.Activity, Resource.Drawable.spinner_selector, _days);
			var adpterYear = new ArrayAdapter(this.Activity, Resource.Drawable.spinner_selector, _Year);
			//adapter.SetDropDownViewResource(Resource.Drawable.spinner_selector);

			spTitle.Adapter = adpterTitle;
			spSuffix.Adapter = adpterSuffix;
			spMonth.Adapter = adpterMonth;
			spDay.Adapter = adpterDay;
			spYear.Adapter = adpterYear;

			_view.FindViewById<Button>(Resource.Id.ActionRegister).Click += ActionRegister;

			_txtFirstname = _view.FindViewById<TextView>(Resource.Id.txtFirstName);
			_txtMiddleName = _view.FindViewById<TextView>(Resource.Id.txtMiddleName);
			_txtLastName = _view.FindViewById<TextView>(Resource.Id.txtLastName);
			_txtEmail = _view.FindViewById<TextView>(Resource.Id.txtEmail);
			_txtPassword = _view.FindViewById<TextView>(Resource.Id.txtPassword);
			_txtPFirstName = _view.FindViewById<TextView>(Resource.Id.txtPFirstName);

			this.SetBinding(() => _requestRegister.FirstName, () => _txtFirstname.Text, BindingMode.TwoWay);
			this.SetBinding(() => _requestRegister.MiddleName, () => _txtMiddleName.Text, BindingMode.TwoWay);
			this.SetBinding(() => _requestRegister.LastName, () => _txtLastName.Text, BindingMode.TwoWay);
			this.SetBinding(() => _requestRegister.Email, () => _txtEmail.Text, BindingMode.TwoWay);
			this.SetBinding(() => _requestRegister.Password, () => _txtPassword.Text, BindingMode.TwoWay);
			this.SetBinding(() => _requestRegister.PrefferedFirstName, () => _txtPFirstName.Text, BindingMode.TwoWay);
		}

		async void ActionRegister(object sender, EventArgs e)
		{
			if (!_requestRegister.IsValidate())
			{
				ShowMessageBox(null, Constants.MSG_REGISTER_REQUEST_INVALID);
				return;
			}
			ShowLoadingView(Constants.MSG_REGISTER);

			var response = await _apiClient.UserRegister(_requestRegister);

			HideLoadingView();

			if (response.Code == "100")
			{
				ShowMessageBox(null, Constants.MSG_REGISTER_SUCCESS);
				_rootVC.ListItemClicked(1);
			}
			else
			{
				ShowMessageBox(null, response.Message);
			}
		}
	}
}
