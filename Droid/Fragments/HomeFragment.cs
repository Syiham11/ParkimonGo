using Android.Views;
using Android.OS;
using Android.Graphics;
using System.Net;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.App.FragmentManager;
using Android.Webkit;
using Java.Interop;
using Android.Widget;
using System.Json;
using Java.Lang;
using Android.App;
using Reino.ClientConfig;

namespace ParkimonGo.Droid
{
	public class HomeFragment : Fragment
	{
		WebView webView;
		GISPayBySpaceListAppInterface _JavascriptAppInterface = null;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.HomeFragment, container, false);

			webView = view.FindViewById<WebView>(Resource.Id.webView);

			TTRegistry.InitRegistry(null);

			webView.SetWebViewClient(new WebViewClient());

			_JavascriptAppInterface = new GISPayBySpaceListAppInterface(Activity, null);
			_JavascriptAppInterface.InitURLForWebViewPage();

			webView.Settings.JavaScriptEnabled = true;
			webView.AddJavascriptInterface(_JavascriptAppInterface, "Android");
			webView.LoadUrl(_JavascriptAppInterface.GetURLForWebViewPage());

			//webView.Settings.AllowContentAccess = true;
			//webView.Settings.EnableSmoothTransition();
			//webView.Settings.LoadsImagesAutomatically = true;
			//webView.Settings.SetGeolocationEnabled(true);
			//webView.SetBackgroundColor(Color.Transparent);
			//webView.ClearCache(true);
			//webView.ClearHistory();
			//webView.LoadUrl(Constants.URL_WEB_MAP);

			return view;
		}

		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}
	}

	//create a class for the webview interface for the GIS map. 
	public class GISPayBySpaceListAppInterface : GISWebViewBaseAppInterface
	{

		/** Instantiate the interface and set the context */
		public GISPayBySpaceListAppInterface(Activity context, FragmentManager fragmentManager)
			: base(context, fragmentManager)
		{
		}

		public string UrlCombine(string url1, string url2)
		{
			if (url1.Length == 0)
			{
				return url2;
			}

			if (url2.Length == 0)
			{
				return url1;
			}

			url1 = url1.TrimEnd('/', '\\');
			url2 = url2.TrimStart('/', '\\');

			return string.Format("{0}/{1}", url1, url2);
		}

		/// <summary>
		/// Attempt to get webview URL from registry
		/// </summary>
		/// <returns></returns>
		public override bool InitURLForWebViewPage()
		{


			// gets specific URL info from registry
			string loBaseURL = TTRegistry.glRegistry.GetRegistryValue(
								  TTRegistry.regSECTION_ISSUE_AP,
								  TTRegistry.regPAYBYSPACE_WEBVIEW_LIST_URL_BASE,
								  TTRegistry.regPAYBYSPACE_WEBVIEW_LIST_URL_BASE_DEFAULT);



			string loCustomerIDAsString = TTRegistry.glRegistry.GetRegistryValue(
														TTRegistry.regSECTION_ISSUE_AP,
														TTRegistry.regPAYBYSPACE_WEBVIEW_LIST_CUSTID,
														TTRegistry.regPAYBYSPACE_WEBVIEW_LIST_CUSTID_DEFAULT);


			_WebViewURL = UrlCombine(loBaseURL, loCustomerIDAsString);


			// return false is not found in registry ?

			return true;
		}

		[Export] // !!! do not work without Export
		[JavascriptInterface] // This is also needed in API 17+
							  // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
							  //the list might have diff business rules, so spin up a seperate class for it
		public void LoadMeterInformation(string meterInfo)
		{

			//fail checks first
			if (string.IsNullOrEmpty(meterInfo))
			{
				Toast.MakeText(_context, "No meter data supplied.", ToastLength.Long).Show();
				return;
			}

			try
			{
				var jsonMeter = JsonValue.Parse(meterInfo);

				//ALAN: they are stryingifying twice when sending the data over to us, so parse it again: Android.LoadMeterInformation(JSON.stringify(JSON.stringify(meterInfo))); remove once they fix this to only stringify once
				//UPDATE(12/28/2015): It looks like they updated this to only do it once:  Android.LoadMeterInformation(JSON.stringify(meterInfo));
				//BUT there were no enforcable meters at this time, so could not test. We shouldnt need the below try/catch and should be safe to remove.
				try
				{
					if (jsonMeter != null) jsonMeter = JsonValue.Parse(jsonMeter);
				}
				catch (Exception ex)
				{
					//couldnt parse it again, hopefully means they fixed the stringify to only occur once. can parse it the same amount of times they stringify it, any more and will throw an exception.
					//LoggingManager.LogApplicationError(ex, "PayBySpaceListFragment: Double Parse", "LoadMeterInformation");
				}

				if (jsonMeter == null)
				{
					Toast.MakeText(_context, "Invalid format: meter data.", ToastLength.Long).Show();
					//LoggingManager.LogApplicationError(null, "PayBySpaceListFragment: Invalid format: meter data", "LoadMeterInformation");
					return;
				}

				//store the data in the context. not saving to the db anymore
				//DroidContext.XmlCfg.SetJsonValueObjectPaySpaceList(jsonMeter);
				//ExternalEnforcementInterfaces.SetWirelessEnforcementMode(ExternalEnforcementInterfaces.TWirelessEnforcementMode.wefPayBySpaceList, jsonMeter);

				#region Samples
				/////////////////////////////////////////////////////////////////////////////////////////////////////////
				////////////////SAMPLES: here are some samples on how to get it out of the context//////////////////////
				/////////////////////////////////////////////////////////////////////////////////////////////////////////
				// //Get the GIS Meter JsonValue Object 
				// var lastGISValue = DroidContext.XmlCfg.GetGISMeterJsonValueObject();

				// //get individual properties from it
				// //Get the meter ID (which is an int value):
				// var meterId = DroidContext.XmlCfg.GetGISMeterPropertyInt("MeterId");
				////get meterName which is a string:
				// var meterName = DroidContext.XmlCfg.GetGISMeterPropertyString("MeterName");
				#endregion


				SwitchToParkingFormForEnforcement(Constants.GIS_PAYBYSPACE_LIST_FRAGMENT_TAG);

			}
			catch (Exception ex)
			{
				//LoggingManager.LogApplicationError(ex, "PayBySpaceListFragment", "LoadMeterInformation");
				Toast.MakeText(_context, "Error loading ticket data.", ToastLength.Long).Show();
			}

		}


		[Export] // !!! do not work without Export
		[JavascriptInterface] // This is also needed in API 17+
							  // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
		public void NavigateToParkingIssueForm(string iJSONWebViewParameters)
		{
			try
			{
				// did they pass something to enforce with?
				if (string.IsNullOrEmpty(iJSONWebViewParameters) == false)
				{
					// if they passed meter data, use it to enforce 
					LoadMeterInformation(iJSONWebViewParameters);
					return;
				}

				// no meter selected, just want to write a ticket without web data
				//ExternalEnforcementInterfaces.ClearGISMeterJsonValueObject();
				SwitchToParkingFormForEnforcement(Constants.GIS_MAP_FRAGMENT_TAG);
			}
			catch (Exception ex)
			{
				//LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToParkingIssueForm");
				Toast.MakeText(_context, "Error in WebViewMapFragment", ToastLength.Long).Show();
			}
		}


	}
}
