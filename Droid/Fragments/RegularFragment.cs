
using Android.OS;
using Android.Views;

namespace ParkimonGo.Droid
{
	public class RegularFragment : BaseFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.RegularFragment, container, false);

			return view;
		}
	}
}
