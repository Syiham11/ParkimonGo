using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Net;

using System;

using Fragment = Android.Support.V4.App.Fragment;
using Android.Webkit;

namespace ParkimonGo.Droid
{
	public class HomeFragment : Fragment
	{
		WebView webView;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.HomeFragment, container, false);

			webView = view.FindViewById<WebView>(Resource.Id.webView);

			webView.Settings.JavaScriptEnabled = true;
			webView.Settings.AllowContentAccess = true;
			webView.Settings.EnableSmoothTransition();
			webView.Settings.LoadsImagesAutomatically = true;
			webView.Settings.SetGeolocationEnabled(true);
			webView.SetBackgroundColor(Color.Transparent);
			webView.ClearCache(true);
			webView.ClearHistory();
			webView.SetWebViewClient(new WebViewClient());
			webView.LoadUrl(Constants.URL_WEB_MAP);

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

		//private class MMWebViewClient : WebViewClient
		//{
		//	LiveTilesHomeAC _act;
		//	LinearLayout _navBar;

		//	public MMWebViewClient(LiveTilesHomeAC act, LinearLayout navBar)
		//	{
		//		_act = act;
		//		_navBar = navBar;
		//	}

		//	public override void OnPageStarted(WebView view, String url, Bitmap favicon)
		//	{
		//		base.OnPageStarted(view, url, favicon);

		//		//_act.ShowLoadingView();
		//	}

		//	public override void OnPageFinished(WebView view, String url)
		//	{
		//		base.OnPageFinished(view, url);

		//		if (url.Contains(Constants.SYMBOL_LOGIN))
		//			_navBar.Visibility = ViewStates.Gone;
		//		else
		//			_navBar.Visibility = ViewStates.Visible;

		//		string cssString = Constants.INJECT_CSS;
		//		string jsString = Constants.INJECT_JS;
		//		string jsWithCSS = string.Format(jsString, cssString);
		//		view.EvaluateJavascript(jsWithCSS, null);

		//		//_act.HideLoadingView();
		//	}
		//}
	}
}
