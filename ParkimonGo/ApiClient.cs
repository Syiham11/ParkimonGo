
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParkimonGo
{
	public class ApiClient
	{
		HttpClient _client = new HttpClient();

		public async Task<CommonMessage> UserRegister(RequestRegister requestRegister)
		{
			var url = Constants.API_BASE + "Register";

			try
			{
				var strRequest = JsonConvert.SerializeObject(requestRegister);
				var content = new StringContent(strRequest, Encoding.UTF8, "application/json");

				var response = await _client.PostAsync(url, content);
				var responseJson = response.Content.ReadAsStringAsync().Result;

				var responseObject = JsonConvert.DeserializeObject<RequestRegister>(responseJson);

				return responseObject.CommonMessages[0];
			}
			catch (Exception e)
			{
				Debug.WriteLine("Exception in UserRegister:" + e.Message);
				return null;
			}
		}

		public async Task<CommonMessage> UserLogin(RequestLogin requestLogin)
		{
			var url = Constants.API_BASE + "Login";

			try
			{
				var strRequest = JsonConvert.SerializeObject(requestLogin);
				var content = new StringContent(strRequest, Encoding.UTF8, "application/json");

				var response = await _client.PostAsync(url, content);
				var responseJson = response.Content.ReadAsStringAsync().Result;

				var responseObject = JsonConvert.DeserializeObject<RequestLogin>(responseJson);

				return responseObject.CommonMessages[0];
			}
			catch (Exception e)
			{
				Debug.WriteLine("Exception in UserLogin:" + e.Message);
				return null;
			}
		}

		public async Task<string> GetGender()
		{
			var url = Constants.API_BASE + "GetGender";

			try
			{
				var response = await _client.GetAsync(url);
				return response.Content.ReadAsStringAsync().Result;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Exception in GetGender:" + e.Message);
				return null;
			}
		}
	}
}
