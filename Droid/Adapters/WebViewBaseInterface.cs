using System;
using System.Json;
using System.Text;
using Android.App;
using Android.Graphics;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Webkit;
using Android.Widget;

using Reino.ClientConfig;
//using Duncan.AI.Droid.Common;
//using Duncan.AI.Droid.Utils.HelperManagers;


using System.Collections.Generic;
using System.Threading.Tasks;

using Java.Interop;


namespace ParkimonGo.Droid
{
    public class GISWebViewBaseAppInterface : Java.Lang.Object
    {
        protected readonly Activity _context;
        protected readonly FragmentManager _fragmentManager;



        // TODO - add support for each page store its own values?
        private const string cnStaticWebViewNameForStoredPreferences = "AIWEBVIEW";



        /// <summary>
        /// Instantiate the interface and set the context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fragmentManager"></param>
        public GISWebViewBaseAppInterface(Activity context, FragmentManager fragmentManager)
        {
            this._context = context;
            _fragmentManager = fragmentManager;
        }


        /// <summary>
        /// WebView URL. Default set in descendant classes; populated at runtime from registry
        /// </summary>
        protected string _WebViewURL = "";

        /// <summary>
        /// Attempt to get webview URL from registry, return false if value not found in config
        /// </summary>
        /// <returns></returns>
        public virtual bool InitURLForWebViewPage()
        {
            // base class reset only - descendant implementation gets specific URL from registry
            _WebViewURL = "";
            return true;
        }

        /// <summary>
        /// Behaviour when selected. populated at runtime from registry
        /// </summary>
        protected bool fReloadOnSelect = true;
        public bool GetReloadOnSelect()
        {
            return fReloadOnSelect;
        }




        // helper method to convert parameters into current system values (as needed)
        public string EvaluateWebViewParameter(string iWebViewParameterSource)
        {
            // look for the prefix
            if (iWebViewParameterSource.StartsWith(TTRegistry.cnWebViewParameterSystemPrefix) == false)
            {
                // no specialty handling, just return original value
                return iWebViewParameterSource;
            }



            switch (iWebViewParameterSource)
            {
                case TTRegistry.cnWebViewParameterCityId:
                    {

                        // for now this is defined in PEMS, not in XML, so use reg value
                        return iWebViewParameterSource;
                    }


                case TTRegistry.cnWebViewParameterOfficerId:
                    {
                        string officerId = null;

                        try
                        {
                            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(_context);
                             officerId = prefs.GetString(Constants.OFFICER_ID, null);
                        }
                        catch (Exception ex)
                        {
                            //LoggingManager.LogApplicationError(ex, "WebViewBaseInterface.EvaluateWebViewParameter " + Constants.OFFICER_ID, ex.TargetSite.Name);
                            System.Console.WriteLine("WebViewBaseInterface.EvaluateWebViewParameter Exception source {0}: {1}", ex.Source, ex.ToString());
                        }

                        return officerId;
                    }

                default:
                    {
                        return iWebViewParameterSource;
                    }
            }


        }


        /// <summary>
        /// Retrieve the saved preference value
        /// </summary>
        /// <returns></returns>
        protected string LoadOneWebViewRunTimeValueFromPreferences( string iWebView, string iParameterName )
        {
            string loResultStr = null;

            try
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(_context);
				loResultStr = prefs.GetString((iWebView + "-" + iParameterName), string.Empty);
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewBaseINterface.GetWebViewRunTimeValueFromPreferences", ex.TargetSite.Name);
                System.Console.WriteLine("WebViewBaseINterface::GetWebViewRunTimeValueFromPreferences Exception source {0}: {1}", ex.Source, ex.ToString());
            }

            return loResultStr;
        }

        /// <summary>
        /// Save the preference value for use
        /// </summary>
        /// <returns></returns>
        private void SaveOneWebViewRunTimeValueToPreferences(string iWebView, string iParameterName, string iParameterValue)
        {
            try
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(_context);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString((iWebView + "-" + iParameterName), iParameterValue);
                //editor.Apply();  // apply() schedules the data to be written asynchronously. It does not inform you about the success of the operation.
                editor.Commit();  //commit() writes the data synchronously (blocking the thread its called from). It then informs you about the success of the operation.
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewBaseInterface.PutWebViewRunTimeValueToPreferences", ex.TargetSite.Name);
                System.Console.WriteLine("WebViewBaseInterface::PutWebViewRunTimeValueToPreferences Exception source {0}: {1}", ex.Source, ex.ToString());
            }
        }


        private void SaveAllWebViewPropertyValuesToPreferences(JsonObject iSourceJsonObject, string iWebViewName )
        {
            try
            {

                if (iSourceJsonObject != null)
                {
                    if (iSourceJsonObject.Keys != null)
                    {
                        foreach (string oneKeyName in iSourceJsonObject.Keys)
                        {
                            string loParameterValue = string.Empty;

                            switch (iSourceJsonObject[oneKeyName].JsonType)
                            {
                                case (JsonType.Array):
                                    {
                                        //return iJsonObject[keyName].ToString();
                                        break;
                                    }
                                case JsonType.Boolean:
                                    {
                                        //return iJsonObject[keyName].ToString();
                                        //if ( *(JsonType.Boolean)iJsonObject[keyName]) == JsonType.Boolean True
                                        break;
                                    }
                                case JsonType.Number:
                                    {
                                        loParameterValue = iSourceJsonObject[oneKeyName].ToString();
                                        break;
                                    }
                                case JsonType.Object:
                                    {
                                        //return iJsonObject[keyName].ToString();
                                        break;
                                    }
                                case JsonType.String:
                                    {
                                        loParameterValue = iSourceJsonObject[oneKeyName];
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }

                            // store it 
                            SaveOneWebViewRunTimeValueToPreferences( iWebViewName, oneKeyName, loParameterValue );
                        }
                    }
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("Failed GetDevicePropertyValuesFromStoredPreferences: " + exp.Message);
                //LoggingManager.LogApplicationError(exp, "WebViewBaseInterface", "GetDevicePropertyValuesFromStoredPreferences");
            }

        }


        private JsonObject LoadAllWebViewPropertyValuesFromStoredPreferences(JsonObject iSourceJsonObject, string iWebViewName )
        {
            JsonObject loGetResult = new JsonObject();

            try
            {
                if (iSourceJsonObject != null)
                {
                    if (iSourceJsonObject.Keys != null)
                    {
                        foreach (string oneKeyName in iSourceJsonObject.Keys)
                        {
                            string iSavedValue = LoadOneWebViewRunTimeValueFromPreferences(iWebViewName, oneKeyName);
                            loGetResult.Add(oneKeyName, iSavedValue);
                        }
                    }
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine("Failed GetDevicePropertyValuesFromStoredPreferences: " + exp.Message);
                //LoggingManager.LogApplicationError(exp, "WebViewBaseInterface", "GetDevicePropertyValuesFromStoredPreferences");
            }

            return loGetResult;
        }








        private int _loadCounter = 0;
        public string GetURLForWebViewPage()
        {

#if _unit_test_force_error
            // for debug - force error on first few trys
            _loadCounter++;
            if ( _loadCounter < 5 )
            {
                return "http://this.isgointobeanerrorforsure" + _loadCounter.ToString() + ".com";
            }
#endif


            return _WebViewURL;
        }

        // AJW - need run this on UI thread and wait for it to finish
        //private async Task StartTicketLayoutOnUIThread(CommonFragment oneFragment)
        //{
        //    int counter = 0;
        //    counter++;


        //    // - is this really waiting for task to be done, or is it just coming back immediately because StartTicketLauout is async?
        //    await Task.Run(() =>
        //    {
        //        //_context.RunOnUiThread(() => oneFragment.StartTicketLayout());

        //        Task.Run(() => _context.RunOnUiThread(() => oneFragment.StartTicketLayout())).ContinueWith(result => counter++);


        //        //    Task.Run(() => _context.RunOnUiThread() ) => oneFragment.StartTicketLayout()).ContinueWith(result => RunOnUiThread(() => oneFragment.StartTicketLayout()));

        //    });


        //    if (counter > 0)
        //    {
        //        switch (counter)
        //        {
        //            case 0:
        //                {
        //                    counter++;
        //                    break;
        //                }
        //            default:
        //                {
        //                    counter--;
        //                    break;
        //                }
        //        }
        //    }
        //}


        private string GetJsonPropertyValueAsStringIgnoreCase(JsonObject iJsonObject, string targetKeyName)
        {
            try
            {
                if (iJsonObject != null)
                {
                    if (string.IsNullOrEmpty(targetKeyName) == false)
                    {
                        if (iJsonObject.Keys != null)
                        {
                            foreach (string oneKeyName in iJsonObject.Keys)
                            {
                                if (oneKeyName.Equals(targetKeyName, StringComparison.InvariantCultureIgnoreCase) == true)
                                {
                                    switch (iJsonObject[oneKeyName].JsonType)
                                    {
                                        case (JsonType.Array):
                                            {
                                                //return iJsonObject[keyName].ToString();
                                                break;
                                            }
                                        case JsonType.Boolean:
                                            {
                                                //return iJsonObject[keyName].ToString();
                                                //if ( *(JsonType.Boolean)iJsonObject[keyName]) == JsonType.Boolean True
                                                break;
                                            }
                                        case JsonType.Number:
                                            {
                                                return iJsonObject[oneKeyName].ToString();
                                            }
                                        case JsonType.Object:
                                            {
                                                //return iJsonObject[keyName].ToString();
                                                break;
                                            }
                                        case JsonType.String:
                                            {
                                                return iJsonObject[oneKeyName];
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }


                                }
                            }
                        }
                    }
                }

                return string.Empty;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Failed GetJsonPropertyValueAsStringIgnoreCase: " + exp.Message);
                return string.Empty;
            }

        }







        private async Task SwitchToParkingEnforcementOnUIThread(string iSwitchingFromFragmentTag)
        {

            try
            {

                _context.RunOnUiThread(() =>
                {
                    //now send the user to the parking form
                    const string formNameToUse = "PARKING";

                    // do this after each ticket is completed and saved... if we do it here, we'll be skipping issue numbers
                    //not here...  DroidContext.ResetControlStatusByStructName(formNameToUse);

                    // but we do need to make these are cleared since we are starting a issue.... TODO - consolidate all of this logic
                    ((HomeActivity)_context).ResetIssueSourceReferences(((HomeActivity)_context).gParkingItemFragmentTag);


                    //CommonFragment dtlFragment = (CommonFragment)((HomeActivity)_context).FindRegisteredFragment(formNameToUse);

                    // here comes the new ticket
                    //dtlFragment.StartTicketLayout();

                    // set the selected navigation back to the parking form. we're on the UI thread for it to work.
                    // is it bad form to cast the context like this?
					((HomeActivity)_context).ChangeToParkingFragment();

                });

            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebView Enforcement", "SwitchToParkingFormForEnfocement: " + ex.Message);
                Toast.MakeText(_context, "Error switching to enforcement.", ToastLength.Long).Show();
            }

        }



        public void SwitchToParkingFormForEnforcement(string iSwitchingFromFragmentTag)
        {
            try
            {

                SwitchToParkingEnforcementOnUIThread(iSwitchingFromFragmentTag);
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "GISMapFragment", "SwitchToParkingFormForEnfocement");
                Toast.MakeText(_context, "Error switching to enforcement.", ToastLength.Long).Show();
            }

        }



        private async Task ShowMenuPopUp_IssueFormSelectionOnUIThread(string iSwitchingFromFragmentTag)
        {
            try
            {
                _context.RunOnUiThread(() =>
                {

                    // bad form to cast the context like this?
                    ((HomeActivity)_context).MenuPopUp_IssueFormSelection();

                });

            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebView Enforcement", "ShowMenuPopUp_IssueFormSelection: " + ex.Message);
                Toast.MakeText(_context, "Error switching to menu.", ToastLength.Long).Show();
            }

        }



        public void ShowMenuPopUp_IssueFormSelection(string iSwitchingFromFragmentTag)
        {
            try
            {

                ShowMenuPopUp_IssueFormSelectionOnUIThread(iSwitchingFromFragmentTag);
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "GISMapFragment", "ShowMenuPopUp_IssueFormSelection");
                Toast.MakeText(_context, "Error switching to menu.", ToastLength.Long).Show();
            }

        }


        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToIssueFormSelectionMenu(string iJSONWebViewParameters)
        {

#if _web_debug_test_

            // save some values
            JsonObject loWebStoreTest = new JsonObject();
            loWebStoreTest.Add("LAT", "123.988");
            loWebStoreTest.Add("LONG", "string");

            PutDeviceLocalDataStoreValue(loWebStoreTest.ToString());



            // get them back
            string loTestResult = GetDeviceLocalDataStoreValue(loWebStoreTest.ToString());
            if (string.IsNullOrEmpty(loTestResult) == false)
            {
                //
                loTestResult = string.Empty;
            }

#endif


            ((HomeActivity)_context).RunOnUiThread(() =>
            {
                try
                {
                    //// did they pass something to enforce with?
                    //if (string.IsNullOrEmpty(iJSONWebViewParameters) == false)
                    //{

                    //}

                    // there was nothing selected, clear
                    //ExternalEnforcementInterfaces.ClearGISMe÷terJsonValueObject();

                    // let them choose a destination
                    ((HomeActivity)_context).MenuPopUp_IssueFormSelection();
                }
                catch (Exception ex)
                {
                    //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToIssueFormSelectionMenu");
                    Toast.MakeText(_context, "Error in WebViewMapFragment", ToastLength.Long).Show();
                }
            });
        }




        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToGISMapWebView(string iJSONWebViewParameters)
        {
            try
            {
                // TODO - kludge to fix old workaround - if we are already on the same page, we need to switch to map instead
                if (this._WebViewURL.ToUpper().Contains("MOBILEMAPVIEW") == true)
                {
                    ((HomeActivity)_context).ChangeToTargetFragmentTag(Constants.GIS_PAYBYSPACE_LIST_FRAGMENT_TAG);
                }
                else
                {
                    ((HomeActivity)_context).ChangeToTargetFragmentTag(Constants.GIS_MAP_FRAGMENT_TAG);
                }
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToPayBySpaceListWebView");
                Toast.MakeText(_context, "Error in WebViewMapFragment", ToastLength.Long).Show();
            }
        }



        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToPayBySpaceListWebView(string iJSONWebViewParameters)
        {
            try
            {
                // TODO - kludge to fix old workaround - if we are already on the same page, we need to switch to map instead
                if (this._WebViewURL.ToUpper().Contains("PAYBYSPACELIST") == true)
                {
                    ((HomeActivity)_context).ChangeToTargetFragmentTag(Constants.GIS_MAP_FRAGMENT_TAG);
                }
                else
                {
                    ((HomeActivity)_context).ChangeToTargetFragmentTag(Constants.GIS_PAYBYSPACE_LIST_FRAGMENT_TAG);
                }
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToPayBySpaceListWebView");
                Toast.MakeText(_context, "Error in WebViewMapFragment", ToastLength.Long).Show();
            }
        }




        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToPayByPlateList(string iJSONWebViewParameters)
        {
            // TO KLUDGE fix - this was only added because webview had wrong reference and needed to be fixed for demo.
            NavigateToPayByPlateListWebView(iJSONWebViewParameters);
        }
        

        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToPayByPlateListWebView(string iJSONWebViewParameters)
        {
            //fail checks first
            if (string.IsNullOrEmpty(iJSONWebViewParameters))
            {
                //Toast.MakeText(_context, "No zone data supplied.", ToastLength.Long).Show();
                //return;
            }

            try
            {
                var jsonWebViewParameters = JsonValue.Parse(iJSONWebViewParameters);

                //ALAN: they are stryingifying twice when sending the data over to us, so parse it again: Android.LoadMeterInformation(JSON.stringify(JSON.stringify(meterInfo))); remove once they fix this to only stringify once
                //UPDATE(12/28/2015): It looks like they updated this to only do it once:  Android.LoadMeterInformation(JSON.stringify(meterInfo));
                //BUT there were no enforcable meters at this time, so could not test. We shouldnt need the below try/catch and should be safe to remove.
                try
                {
                    if (jsonWebViewParameters != null) jsonWebViewParameters = JsonValue.Parse(jsonWebViewParameters);
                }
                catch (Exception ex)
                {
                    //couldnt parse it again, hopefully means they fixed the stringify to only occur once. can parse it the same amount of times they stringify it, any more and will throw an exception.
                    //LoggingManager.LogApplicationError(ex, "NavigateToPayByPlateListWebView: Double Parse", "LoadMeterInformation");
                }

                if (jsonWebViewParameters == null)
                {
                    Toast.MakeText(_context, "Invalid format: meter data.", ToastLength.Long).Show();
                    //LoggingManager.LogApplicationError(null, "NavigateToPayByPlateListWebView: Invalid format: meter data", "NavigateToPayByPlateListWebView");
                    return;
                }


                JsonObject _JsonObjectWebViewParameters = null;

                // convert to an object
                if (jsonWebViewParameters != null)
                {
                    try
                    {
                        _JsonObjectWebViewParameters = jsonWebViewParameters as JsonObject;
                    }
                    catch (Exception exp)
                    {
                        _JsonObjectWebViewParameters = null;
                        Console.WriteLine("Failed to cast Json Value: " + exp.Message);
                    }
                }
                else
                {
                    _JsonObjectWebViewParameters = null;
                }


                // TODO: iterate through and build a parameter string list

                // see if we were passed a zone id
                //var loZoneId = GetJsonPropertyValueAsStringIgnoreCase(_JsonObjectWebViewParameters, Constants.WebViewRunTimeParameter_ZoneID);

                //if (string.IsNullOrEmpty(loZoneId) == true)
                //{
                //    // if its not defined, we still need to clear out any old value
                //    loZoneId = string.Empty;
                //}

                // set it for use in URL build
                //SaveOneWebViewRunTimeValueToPreferences(Constants.GIS_PAYBYPLATE_LIST_FRAGMENT_TAG, Constants.WebViewRunTimeParameter_ZoneID, loZoneId);

                // load it up
                ((HomeActivity)_context).ChangeToTargetFragmentTag(Constants.GIS_PAYBYPLATE_LIST_FRAGMENT_TAG);
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToPayByPlateListWebView");
                Toast.MakeText(_context, "Error in WebViewMapFragment", ToastLength.Long).Show();
            }
        }





        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToCameraAction(string iJSONWebViewParameters)
        {

            ((HomeActivity)_context).RunOnUiThread(() =>
            {
                try
                {
                    // let them choose a destination
                    ((HomeActivity)_context).MenuPopUp_LaunchCamera();
                }
                catch (Exception ex)
                {
                    //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToCameraAction");
                    Toast.MakeText(_context, "Error in WebViewMapFragment.NavigateToCameraAction", ToastLength.Long).Show();
                }
            });
        }

        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void NavigateToWebViewByTagName(string iJSONWebViewParameters)
        {
            try
            {
                //((HomeActivity)_context).ChangeToTargetWebViewByTagName(iJSONWebViewParameters);
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "NavigateToPayByPlateListWebView");
                Toast.MakeText(_context, "Error in WebViewMapFragment", ToastLength.Long).Show();
            }
        }




        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
         //to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public string GetDeviceCurrentLocation(string iJSONWebViewParameters)
        {
			return "aaa";
            //((HomeActivity)_context).RunOnUiThread(() =>
            //{
            //    try
            //    {
            //        JsonObject loLocationResult = new JsonObject();
            //        Android.Locations.Location oneCurrentLocation = DroidContext.GetCurrentLocation(); //LocationUpdateListener.GetLastUpdatedLocation();
            //        if (oneCurrentLocation != null)
            //        {
            //            loLocationResult.Add("LAT", oneCurrentLocation.Latitude.ToString());
            //            loLocationResult.Add("LONG", oneCurrentLocation.Longitude.ToString());
            //        }

            //        return loLocationResult.ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        //LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "GetDeviceCurrentLocation");
            //        Toast.MakeText(_context, "Error in WebViewMapFragment.GetDeviceCurrentLocation", ToastLength.Long).Show();
            //        return "GetDeviceCurrentLocation Error: " + ex.Message;
            //    }
            //}  //);
        }






        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public void PutDeviceLocalDataStoreValues(string iJSONWebViewParameters)
        {
            try
            {
                string loWebViewName = cnStaticWebViewNameForStoredPreferences;

                var jsonWebViewParameters = JsonValue.Parse(iJSONWebViewParameters);

                //ALAN: they are stryingifying twice when sending the data over to us, so parse it again: Android.LoadMeterInformation(JSON.stringify(JSON.stringify(meterInfo))); remove once they fix this to only stringify once
                //UPDATE(12/28/2015): It looks like they updated this to only do it once:  Android.LoadMeterInformation(JSON.stringify(meterInfo));
                //BUT there were no enforcable meters at this time, so could not test. We shouldnt need the below try/catch and should be safe to remove.
                try
                {
                    if (jsonWebViewParameters != null) jsonWebViewParameters = JsonValue.Parse(jsonWebViewParameters);
                }
                catch (Exception ex)
                {
                    //couldnt parse it again, hopefully means they fixed the stringify to only occur once. can parse it the same amount of times they stringify it, any more and will throw an exception.
                    //LoggingManager.LogApplicationError(ex, "NavigateToPayByPlateListWebView: Double Parse", "LoadMeterInformation");
                }

                if (jsonWebViewParameters == null)
                {
                    Toast.MakeText(_context, "Invalid format: PutDeviceLocalDataStoreValue data.", ToastLength.Long).Show();
                    //LoggingManager.LogApplicationError(null, "PutDeviceLocalDataStoreValue: Invalid format: source data", "PutDeviceLocalDataStoreValue");
                    return;
                }


                JsonObject _JsonObjectWebViewParameters = null;

                // convert to an object
                if (jsonWebViewParameters != null)
                {
                    try
                    {
                        _JsonObjectWebViewParameters = jsonWebViewParameters as JsonObject;
                    }
                    catch (Exception exp)
                    {
                        _JsonObjectWebViewParameters = null;
                        Console.WriteLine("Failed to cast Json Value: " + exp.Message);
                    }
                }
                else
                {
                    _JsonObjectWebViewParameters = null;
                }


                SaveAllWebViewPropertyValuesToPreferences(_JsonObjectWebViewParameters, loWebViewName);
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewBaseInterface", "PutDeviceLocalDataStoreValue");
                Toast.MakeText(_context, "Error in WebViewBaseInterface.PutDeviceLocalDataStoreValue", ToastLength.Long).Show();
            }
        }


        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        public string GetDeviceLocalDataStoreValues(string iJSONWebViewParameters)
        {

            string loResultStr = string.Empty;

            try
            {
                string loWebViewName = cnStaticWebViewNameForStoredPreferences;

                var jsonWebViewParameters = JsonValue.Parse(iJSONWebViewParameters);

                //ALAN: they are stryingifying twice when sending the data over to us, so parse it again: Android.LoadMeterInformation(JSON.stringify(JSON.stringify(meterInfo))); remove once they fix this to only stringify once
                //UPDATE(12/28/2015): It looks like they updated this to only do it once:  Android.LoadMeterInformation(JSON.stringify(meterInfo));
                //BUT there were no enforcable meters at this time, so could not test. We shouldnt need the below try/catch and should be safe to remove.
                try
                {
                    if (jsonWebViewParameters != null) jsonWebViewParameters = JsonValue.Parse(jsonWebViewParameters);
                }
                catch (Exception ex)
                {
                    //couldnt parse it again, hopefully means they fixed the stringify to only occur once. can parse it the same amount of times they stringify it, any more and will throw an exception.
                    //LoggingManager.LogApplicationError(ex, "NavigateToPayByPlateListWebView: Double Parse", "LoadMeterInformation");
                }

                if (jsonWebViewParameters == null)
                {
                    Toast.MakeText(_context, "Invalid format: GetDeviceLocalDataStoreValue data.", ToastLength.Long).Show();
                    //LoggingManager.LogApplicationError(null, "GetDeviceLocalDataStoreValue: Invalid format: source data", "GetDeviceLocalDataStoreValue");
                    return loResultStr;
                }


                JsonObject _JsonObjectWebViewParameters = null;

                // convert to an object
                if (jsonWebViewParameters != null)
                {
                    try
                    {
                        _JsonObjectWebViewParameters = jsonWebViewParameters as JsonObject;
                    }
                    catch (Exception exp)
                    {
                        _JsonObjectWebViewParameters = null;
                        Console.WriteLine("Failed to cast Json Value: " + exp.Message);
                    }
                }
                else
                {
                    _JsonObjectWebViewParameters = null;
                }


                JsonObject loResultObj = LoadAllWebViewPropertyValuesFromStoredPreferences(_JsonObjectWebViewParameters, loWebViewName);

               loResultStr = loResultObj.ToString();
            }
            catch (Exception ex)
            {
                //LoggingManager.LogApplicationError(ex, "WebViewBaseInterface", "GetDeviceLocalDataStoreValue");
                Toast.MakeText(_context, "Error in WebViewBaseInterface.GetDeviceLocalDataStoreValue", ToastLength.Long).Show();
            }


            return loResultStr;
        }

       



#if _include_wip_

        [Export] // !!! do not work without Export
        [JavascriptInterface] // This is also needed in API 17+
        // to become consistent with Java/JS interop convention, the argument cannot be System.String. 
        //public JsonObject GetDeviceCurrentLocation(string iJSONWebViewParameters)
        public string ClearDeviceLocalDataStoreValues(string iJSONWebViewParameters)
        {
           // ((HomeActivity)_context).RunOnUiThread(() =>
            {
                try
                {
                    JsonObject loLocationResult = new JsonObject();
                    Android.Locations.Location oneCurrentLocation = DroidContext.GetCurrentLocation(); //LocationUpdateListener.GetLastUpdatedLocation();
                    if (oneCurrentLocation != null)
                    {
                        loLocationResult.Add("LAT", oneCurrentLocation.Latitude.ToString());
                        loLocationResult.Add("LONG", oneCurrentLocation.Longitude.ToString());
                    }

                    return loLocationResult.ToString();
                }
                catch (Exception ex)
                {
                    LoggingManager.LogApplicationError(ex, "WebViewMapFragment", "GetDeviceCurrentLocation");
                    Toast.MakeText(_context, "Error in WebViewMapFragment.GetDeviceCurrentLocation", ToastLength.Long).Show();
                    return "GetDeviceCurrentLocation Error: " + ex.Message;
                }
            }  //);
        }

#endif

    }

}

