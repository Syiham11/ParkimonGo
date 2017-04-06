
using System;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	public class RegularFragment : BaseFragment
	{
		TextView txtDuration, txtPrice;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.RegularFragment, container, false);

			view.FindViewById(Resource.Id.ActionMinusHours).Click += ActionMinusHours;
			view.FindViewById(Resource.Id.ActionPlusHours).Click += ActionPlusHours;

			txtDuration = view.FindViewById<TextView>(Resource.Id.txtDuration);
			txtPrice = view.FindViewById<TextView>(Resource.Id.txtPrice);

			return view;
		}

		void ActionMinusHours(object sender, EventArgs e)
		{
			 var duration = StringToTMin(txtDuration.Text);

			 var resultMin = duration - 15;

			 if (resultMin < 0) return;

			 txtDuration.Text = TMinToString(resultMin);
			 txtPrice.Text = "Rs. " + (resultMin * 10 / 60f).ToString();
		}

		void ActionPlusHours(object sender, EventArgs e)
		{
			var duration = StringToTMin(txtDuration.Text);

			var resultMin = duration + 15;

			txtDuration.Text = TMinToString(resultMin);
			txtPrice.Text = "Rs. " + (resultMin * 10 / 60f).ToString();
		}

		int StringToTMin(string strTime)
		{
			var arrTimes = strTime.Split(new char[] { ':' });

			var hrs = int.Parse(arrTimes[0]);
			var min = int.Parse(arrTimes[1]);

			return hrs * 60 + min;
		}
		string TMinToString(int tMin)
		{
			int hour = tMin / 60;
			int min = tMin % 60;
			return hour + ":" + min.ToString("D2");
		}
	}
}
