using Android.App;
using Android.OS;
using Android.Widget;

using Android.Support.V4.Widget;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;

namespace ParkimonGo.Droid
{
	[Activity(Label = "ParkimonGo", MainLauncher = true, Icon = "@drawable/icon")]
	public class HomeActivity : BaseActivity
	{
		ListView mDrawerList;
		public RelativeLayout mDrawerPane;
		public DrawerLayout mDrawerLayout;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.HomeLayout);

			initialSettings();
		}

		private void initialSettings()
		{
			//Load Home fragment
			Fragment fragment = new HomeFragment();
			FragmentTransaction fragmentTx = SupportFragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			mDrawerLayout = (DrawerLayout)FindViewById(Resource.Id.drawerLayout);
			mDrawerPane = (RelativeLayout)FindViewById(Resource.Id.drawerPane);
			mDrawerList = (ListView)FindViewById(Resource.Id.menuList);

			string[] navList = { "Parking", "Find Parking", "Reserve Parking", "My Account", "Buy Permits", "Log out" };

			MenuListAdapter adapter = new MenuListAdapter(this, navList);
			mDrawerList.Adapter = adapter;

			// Drawer Item click listeners
			mDrawerList.ItemClick += (sender, args) => ListItemClicked(args.Position);

			FindViewById<TextView>(Resource.Id.ActionLogin).Click += (sender, e) => ListItemClicked(5);
			FindViewById<ImageView>(Resource.Id.ActionOpenMenu).Click += (sender, e) => mDrawerLayout.OpenDrawer(mDrawerPane);
		}

		public void ListItemClicked(int position)
		{
			Fragment fragment = null;

			switch (position)
			{
				case 1:
					fragment = new HomeFragment();
					break;
				case 2:
					fragment = new ReserveFragment(0);
					break;
				case 3:
					fragment = new RegisterFragment();
					break;
				case 4:
					fragment = new ReserveFragment(1);
					break;
				case 5:
					fragment = new LoginFragment();
					break;
				default:
					return;
			}

			FragmentTransaction fragmentTx = SupportFragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			mDrawerList.SetItemChecked(position, true);
			mDrawerLayout.CloseDrawer(mDrawerPane);
		}




		#region test
		public string gParkingItemFragmentTag = string.Empty;

		public void ChangeToParkingFragment()
		{
			ShowMessageBox(null, "ChangeToParkingFragment");
		}
		public void ResetIssueSourceReferences(string iTargetFragmenTag)
		{
			ShowMessageBox(null, "ResetIssueSourceReferences");
		}
		public void MenuPopUp_IssueFormSelection()
		{
			ShowMessageBox(null, "MenuPopUp_IssueFormSelection");
		}
		public void ChangeToTargetFragmentTag(string iTargetFragmentTag)
		{
			ShowMessageBox(null, "ChangeToTargetFragmentTag");
		}
		public void MenuPopUp_LaunchCamera()
		{
			ShowMessageBox(null, "MenuPopUp_LaunchCamera");
		}
		#endregion
	}
}

