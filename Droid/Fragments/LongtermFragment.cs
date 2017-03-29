
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace ParkimonGo.Droid
{
	public class LongtermFragment : BaseFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.LongtermFragment, container, false);

			var listView = view.FindViewById<ListView>(Resource.Id.listView);

			var parkings = new List<Parking>();
			parkings.Add(new Parking("10 Days", "100 Rs."));
			parkings.Add(new Parking("20 Days", "200 Rs."));
			parkings.Add(new Parking("30 Days", "300 Rs."));

			var adapter = new LongtermListAdapter(this.Activity, parkings);
			listView.Adapter = adapter;
			adapter.NotifyDataSetChanged();

			return view;
		}
	}
}
