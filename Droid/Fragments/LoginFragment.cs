
using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;

namespace ParkimonGo.Droid
{
	public class LoginFragment : BaseFragment
	{
		RequestLogin _requestLogin = new RequestLogin();

		HomeActivity _rootVC;
		View _view;

		TextView _txtEmail, _txtPassword;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_view = inflater.Inflate(Resource.Layout.LoginFragment, container, false);

			_rootVC = this.Activity as HomeActivity;

			InitUISettings();

			return _view;
		}

		void InitUISettings()
		{
			_view.FindViewById<Button>(Resource.Id.ActionLogin).Click += ActionLogin;
			_view.FindViewById<Button>(Resource.Id.ActionRegister).Click += (sender, e) => _rootVC.ListItemClicked(3);

			_txtEmail = _view.FindViewById<TextView>(Resource.Id.txtEmail);
			_txtPassword = _view.FindViewById<TextView>(Resource.Id.txtPassword);

			this.SetBinding(() => _requestLogin.Email, () => _txtEmail.Text, BindingMode.TwoWay);
			this.SetBinding(() => _requestLogin.Password, () => _txtPassword.Text, BindingMode.TwoWay);

			var contentView = _view.FindViewById<LinearLayout>(Resource.Id.contentView);
			var childs = GetAllChildren(contentView);
			for (int i = 0; i < childs.Count; i++)
			{
				if (childs[i] is EditText)
					((EditText)childs[i]).TextChanged += (s, e) => { };
			}
		}
		List<View> GetAllChildren(View view)
		{
			if (!(view is ViewGroup))
			{
				List<View> viewArrayList = new List<View>();
				viewArrayList.Add(view);
				return viewArrayList;
			}

			List<View> result = new List<View>();

			ViewGroup vg = (ViewGroup)view;
			for (int i = 0; i < vg.ChildCount; i++)
			{
				View child = vg.GetChildAt(i);
				List<View> viewArrayList = new List<View>();
				viewArrayList.Add(view);
				viewArrayList.AddRange(GetAllChildren(child));
				result.AddRange(viewArrayList);
			}
			return result;
		}

		async void ActionLogin(object sender, EventArgs e)
		{
			if (!_requestLogin.IsValidate())
			{
				ShowMessageBox(null, Constants.MSG_REGISTER_REQUEST_INVALID);
				return;
			}
			ShowLoadingView(Constants.MSG_LOGIN);

			var response = await _apiClient.UserLogin(_requestLogin);

			HideLoadingView();

			if (response.Code == "100")
			{
				ShowMessageBox(null, Constants.MSG_LOGIN_SUCCESS);
				_rootVC.ListItemClicked(1);
			}
			else
			{
				ShowMessageBox(null, response.Message);
			}
		}
	}
}
