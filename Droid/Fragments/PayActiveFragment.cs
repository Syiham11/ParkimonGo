
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	public class PayActiveFragment : BaseFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.PayActiveFragment, container, false);

			var listView = view.FindViewById<ListView>(Resource.Id.listView);

			var payments = new List<Payment>();
			payments.Add(new Payment("2", "Descrition \n Dubai", 159872, DateTime.Now));
			payments.Add(new Payment("d", "Descrition \n UAE AD", 159872, DateTime.Now));
			payments.Add(new Payment("2", "Descrition \n AAA", 159872, DateTime.Now));

			var adapter = new PayActiveAdapter(this.Activity, payments);
			listView.Adapter = adapter;
			adapter.NotifyDataSetChanged();

			return view;
		}
	}
}
