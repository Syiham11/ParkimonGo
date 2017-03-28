
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;

using Fragment = Android.Support.V4.App.Fragment;

namespace ParkimonGo.Droid
{
	[Activity(Label = "PayActivity")]
	public class PayActivity : BaseActivity
	{
		ViewPager _pager;

		TextView _txtTab1, _txtTab2;
		LinearLayout _barTab1, _barTab2;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.PayLayout);

			initialSettings();
		}

		void initialSettings()
		{
			_txtTab1 = FindViewById<TextView>(Resource.Id.txtTab1);
			_txtTab2 = FindViewById<TextView>(Resource.Id.txtTab2);

			_barTab1 = FindViewById<LinearLayout>(Resource.Id.barTab1);
			_barTab2 = FindViewById<LinearLayout>(Resource.Id.barTab2);

			FindViewById<LinearLayout>(Resource.Id.ActionTabActive).Click += (sender, args) => { SetPage(0); };
			FindViewById<LinearLayout>(Resource.Id.ActionTabExpired).Click += (sender, args) => { SetPage(1); };

			List<Fragment> fragments = new List<Fragment>();
			fragments.Add(new PayActiveFragment());
			fragments.Add(new PayExpiredFragment());

			TabAdapter adaptor = new TabAdapter(SupportFragmentManager, fragments);

			_pager = FindViewById<ViewPager>(Resource.Id.eTicketPager);
			_pager.Adapter = adaptor;
			_pager.PageSelected += PagerOnPageSelected;

			SetPage(0);
			SetSelect(0);
		}

		private void SetPage(int position)
		{
			_pager.SetCurrentItem(position, true);
		}

		private void PagerOnPageSelected(object sender, ViewPager.PageSelectedEventArgs e)
		{
			SetSelect(e.Position);
		}

		private void SetSelect(int position)
		{
			_txtTab1.SetTextColor(Color.Gray);
			_txtTab2.SetTextColor(Color.Gray);

			_barTab1.SetBackgroundColor(Color.Transparent);
			_barTab2.SetBackgroundColor(Color.Transparent);

			switch (position)
			{
				case 0:
					_txtTab1.SetTextColor(_cOrange);
					_barTab1.SetBackgroundColor(_cOrange);
					break;
				case 1:
					_txtTab2.SetTextColor(_cOrange);
					_barTab2.SetBackgroundColor(_cOrange);
					break;
			}
		}
	}
}
