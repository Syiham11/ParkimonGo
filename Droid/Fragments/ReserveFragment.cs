
using System.Collections.Generic;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

using Fragment = Android.Support.V4.App.Fragment;

namespace ParkimonGo.Droid
{
	public class ReserveFragment : Fragment
	{
		ViewPager _pager;

		TextView _txtTab1, _txtTab2, _txtTab3;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.ReserveFragment, container, false);

			_txtTab1 = view.FindViewById<TextView>(Resource.Id.txtTab1);
			_txtTab2 = view.FindViewById<TextView>(Resource.Id.txtTab2);
			_txtTab3 = view.FindViewById<TextView>(Resource.Id.txtTab3);

			view.FindViewById<RelativeLayout>(Resource.Id.ActionTabHistory).Click += (sender, args) => { SetPage(0); };
			view.FindViewById<RelativeLayout>(Resource.Id.ActionTabETicket).Click += (sender, args) => { SetPage(1); };
			view.FindViewById<RelativeLayout>(Resource.Id.ActionTabFines).Click += (sender, args) => { SetPage(2); };

			List<Fragment> fragments = new List<Fragment>();
			fragments.Add(new HistoryFragment());
			fragments.Add(new ETicketFragment());
			fragments.Add(new FinesFragment());

			TabAdapter adaptor = new TabAdapter(FragmentManager, fragments);

			_pager = view.FindViewById<ViewPager>(Resource.Id.pager);
			_pager.Adapter = adaptor;
			_pager.PageSelected += PagerOnPageSelected;
			
			SetPage(1);

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
			_txtTab1.SetTextColor(Color.Gray);
			_txtTab2.SetTextColor(Color.Gray);
			_txtTab3.SetTextColor(Color.Gray);

			switch (position)
			{
				case 0:
					_txtTab1.SetTextColor(Color.Orange);
					break;
				case 1:
					_txtTab2.SetTextColor(Color.Orange);
					break;
				case 2:
					_txtTab3.SetTextColor(Color.Orange);
					break;
			}
		}
	}
}
