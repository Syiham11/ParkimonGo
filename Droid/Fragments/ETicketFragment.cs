
using System.Collections.Generic;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

using Fragment = Android.Support.V4.App.Fragment;

namespace ParkimonGo.Droid
{
	public class ETicketFragment : BaseFragment
	{
		int _tabIndex;

		ViewPager _pager;

		TextView _txtTab1, _txtTab2, _txtTab3;
		LinearLayout _barTab1, _barTab2, _barTab3;

		public ETicketFragment(int tabIndex)
		{
			_tabIndex = tabIndex;
		}
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.ETicketFragment, container, false);

			_txtTab1 = view.FindViewById<TextView>(Resource.Id.txtTab1);
			_txtTab2 = view.FindViewById<TextView>(Resource.Id.txtTab2);
			_txtTab3 = view.FindViewById<TextView>(Resource.Id.txtTab3);

			_barTab1 = view.FindViewById<LinearLayout>(Resource.Id.barTab1);
			_barTab2 = view.FindViewById<LinearLayout>(Resource.Id.barTab2);
			_barTab3 = view.FindViewById<LinearLayout>(Resource.Id.barTab3);

			view.FindViewById<LinearLayout>(Resource.Id.ActionTabHistory).Click += (sender, args) => { SetPage(0); };
			view.FindViewById<LinearLayout>(Resource.Id.ActionTabETicket).Click += (sender, args) => { SetPage(1); };
			view.FindViewById<LinearLayout>(Resource.Id.ActionTabFines).Click += (sender, args) => { SetPage(2); };

			List<Fragment> fragments = new List<Fragment>();
			fragments.Add(new VIPFragment());
			fragments.Add(new RegularFragment());
			fragments.Add(new LongtermFragment());

			TabAdapter adaptor = new TabAdapter(FragmentManager, fragments);

			_pager = view.FindViewById<ViewPager>(Resource.Id.eTicketPager);
			_pager.Adapter = adaptor;
			_pager.PageSelected += PagerOnPageSelected;

			SetPage(_tabIndex);

			return view;
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
			_txtTab1.SetTextColor(_cGray);
			_txtTab2.SetTextColor(_cGray);
			_txtTab3.SetTextColor(_cGray);

			_barTab1.SetBackgroundColor(Color.Transparent);
			_barTab2.SetBackgroundColor(Color.Transparent);
			_barTab3.SetBackgroundColor(Color.Transparent);

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
				case 2:
					_txtTab3.SetTextColor(_cOrange);
					_barTab3.SetBackgroundColor(_cOrange);
					break;
			}
		}
	}
}
