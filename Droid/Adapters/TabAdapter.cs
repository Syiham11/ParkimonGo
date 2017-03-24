
using System.Collections.Generic;
using Android.Support.V4.App;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace ParkimonGo.Droid
{
	public class TabAdapter : FragmentStatePagerAdapter
	{
		List<Fragment> _items;

		public TabAdapter(FragmentManager fm, List<Fragment> children) : base(fm)
		{
			_items = children;
		}

		public override int Count
		{
			get { return _items.Count; }
		}

		public override Fragment GetItem(int position)
		{
			return _items[position];
		}
	}
}
