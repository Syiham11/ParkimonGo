using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace ParkimonGo.Droid
{
	class MenuListAdapter : BaseAdapter
	{

		Context mContext;
		string[] mNavItems;

		Typeface nexaBold;
		Typeface nexaLight;

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
			{
				convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.MenuItem, null, false);
			}
			var titleView = (TextView)convertView.FindViewById(Resource.Id.title);
			var navItem = GetNavItem(position);
			titleView.Text = navItem;

			if (position == 1 || position == 2)
			{
				titleView.SetTypeface(nexaBold, TypefaceStyle.Normal);
				titleView.SetTextSize(ComplexUnitType.Dip, 13);
				convertView.SetMinimumHeight(80);
			}
			else
			{
				titleView.SetTypeface(nexaLight, TypefaceStyle.Normal);
				titleView.SetTextSize(ComplexUnitType.Dip, 18);
				convertView.SetMinimumHeight(80);
			}
			//else {
			//	titleView.SetTypeface(nexaLight, TypefaceStyle.Normal);
			//	titleView.SetTextSize(ComplexUnitType.Dip, 12);
			//	convertView.SetMinimumHeight(80);
			//}

			return convertView;
		}
	}
}
