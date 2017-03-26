
using System;
using System.Collections.Generic;
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
		const string API_BASE = "https://pems-sit.pemsportal.com/MobilePaymentApp.MobilePayment.svc/";

		HttpClient _client = new HttpClient();

		public ApiClient()
		{
			//System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;

		}

		public async Task<string> UserRegister()
		{
			var url = API_BASE + "Register";

			try
			{
				var strRequest = JsonConvert.SerializeObject(new RequestRegister());
				//var requestBody = new StringContent(strRequest);

				var content = new StringContent(strRequest, Encoding.UTF8, "application/json");

				var response = await _client.PostAsync(url, content);
				return response.Content.ReadAsStringAsync().Result;
			}
			catch (Exception e)
			{
				//Console.WriteLine("Exception in SendPushDeviceToken:" + e.Message);
				return null;
			}
		}

		public async Task<string> GetGender()
		{
			var url = API_BASE + "GetGender";

			try
			{
				var response = await _client.GetAsync(url);
				return response.Content.ReadAsStringAsync().Result;
			}
			catch (Exception e)
			{
				//Console.WriteLine("Exception in SendPushDeviceToken:" + e.Message);
				return null;
			}
		}
	}
}
