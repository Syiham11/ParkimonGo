using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;

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

			//Load Slide menu
			//var avatar = (ImageView)FindViewById(Resource.Id.avatar);
			//if (Settings.User.photoURL != null && Settings.User.photoURL != "")
			//{
			//	Koush.UrlImageViewHelper.SetUrlDrawable(avatar, Settings.User.photoURL);
			//}

			//TextView username = (TextView)FindViewById(Resource.Id.menu_userName);
			//username.Text = "Hey, " + Settings.User.name;

			mDrawerLayout = (DrawerLayout)FindViewById(Resource.Id.drawerLayout);
			mDrawerPane = (RelativeLayout)FindViewById(Resource.Id.drawerPane);
			mDrawerList = (ListView)FindViewById(Resource.Id.menuList);

			string[] navList = { "Parking", "Find Parking", "Reserve Parking", "My Account", "Buy Permits", "Log out" };

			MenuListAdapter adapter = new MenuListAdapter(this, navList);
			mDrawerList.Adapter = adapter;

			// Drawer Item click listeners
			mDrawerList.ItemClick += (sender, args) => ListItemClicked(args.Position);

			mDrawerLayout.OpenDrawer(mDrawerPane);
		}

		private void ListItemClicked(int position)
		{
			Fragment fragment = null;

			switch (position)
			{
				case 0:
					fragment = new HomeFragment();
					break;
				//case 1:
				//	fragment = new UserDailyDealsFragment();
				//	break;
				//case 2:
				//	fragment = new UserOffersFragment();
				//	break;
				//case 3:
				//	fragment = new UserFavoritesFragment();
				//	break;
				//case 4:
				//	fragment = new UserProfileFragment();
				//	break;
				default:
					return;
			}

			FragmentTransaction fragmentTx = SupportFragmentManager.BeginTransaction();
			fragmentTx.Replace(Resource.Id.mainFragmentContent, fragment).Commit();

			mDrawerList.SetItemChecked(position, true);
			mDrawerLayout.CloseDrawer(mDrawerPane);
		}
	}
}

