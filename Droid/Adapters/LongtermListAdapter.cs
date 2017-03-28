
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	class LongtermListAdapter : BaseAdapter
	{
		Context mContext;
		List<Parking> mParkings;

		public LongtermListAdapter(Context context, List<Parking> parkings)
		{
			mContext = context;
			mParkings = parkings;
		}

		public override int Count
		{
			get
			{
				return mParkings.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public Parking GetParking(int position)
		{
			return mParkings[position];
		}

		override public long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.ItemParking, null);
			convertView.FindViewById(Resource.Id.ActionPay).Click += ActionPay;

			var parking = GetParking(position);
			convertView.FindViewById<TextView>(Resource.Id.txtPeriod).Text = parking.period;
			convertView.FindViewById<TextView>(Resource.Id.txtFees).Text = parking.fees;

			return convertView;
		}

		void ActionPay(object sender, EventArgs e)
		{
			var activity = mContext as HomeActivity;
			activity.StartActivity(new Intent(activity, typeof(PayActivity)));
			//mSuperActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
		}
	}
}
