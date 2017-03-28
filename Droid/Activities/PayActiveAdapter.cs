
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
	class PayActiveAdapter : BaseAdapter
	{
		Context mContext;
		List<Payment> mPayments;

		public PayActiveAdapter(Context context, List<Payment> payments)
		{
			mContext = context;
			mPayments = payments;
		}

		public override int Count
		{
			get
			{
				return mPayments.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public Payment GetPayment(int position)
		{
			return mPayments[position];
		}

		override public long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			convertView = LayoutInflater.From(mContext).Inflate(Resource.Layout.ItemPaymentActive, null);
			convertView.FindViewById(Resource.Id.ActionPay).Click += ActionPay;

			var payment = GetPayment(position);
			convertView.FindViewById<TextView>(Resource.Id.txtNo).Text = payment.no;
			convertView.FindViewById<TextView>(Resource.Id.txtDescription).Text = payment.description;
			convertView.FindViewById<TextView>(Resource.Id.txtId).Text = payment.id.ToString();
			convertView.FindViewById<TextView>(Resource.Id.txtDate).Text = DateTime.Now.ToString();
			convertView.FindViewById<TextView>(Resource.Id.txtDescription).Text = payment.description;

			return convertView;
		}

		void ActionPay(object sender, EventArgs e)
		{
			//var activity = mContext as HomeActivity;
			//activity.StartActivity(new Intent(activity, typeof(PayActivity)));
			//mSuperActivity.OverridePendingTransition(Resource.Animation.fromLeft, Resource.Animation.toRight);
		}
	}
}
