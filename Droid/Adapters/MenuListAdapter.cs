using Android.Content;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	class MenuListAdapter : BaseAdapter
	{
		Context mContext;
		string[] mNavItems;

		public MenuListAdapter(Context context, string[] navItems)
		{
			mContext = context;
			mNavItems = navItems;
		}

		public override int Count
		{
			get
			{
				return mNavItems.Length;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public string GetNavItem(int position)
		{
			return mNavItems[position];
		}

		override public long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			if (convertView == null)
				convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.MenuItem, null, false);
			
			var navItem = GetNavItem(position);

			View returnView;
			if (position == 1 || position == 2)
			{
				returnView = convertView.FindViewById(Resource.Id.itemChild);
				returnView.FindViewById<TextView>(Resource.Id.cTitle).Text = navItem;
			}
			else
			{
				returnView = convertView.FindViewById(Resource.Id.itemParent);
				returnView.FindViewById<TextView>(Resource.Id.pTitle).Text = navItem;
			}

			return returnView;
		}
	}
}
