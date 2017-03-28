using System;
using System.Collections.Generic;

namespace ParkimonGo
{
	public class RequestActivity
	{
		public RequestActivity()
		{
			//ActivityId = 1;
			//ActivityName = "Test Activity";
			//CustomerId = 9223372036854775807;
			//Email = "anangas3456@emqos.com";
			//Token = "String content";
			//TokenType = 2147483647;
			//UserRegistrationId = 9223372036854775807;
		}

		//public long ActivityId { get; set; }
		//public string ActivityName { get; set; }
		//public long CustomerId { get; set; }
		//public string Email { get; set; }
		//public string Token { get; set; }
		//public long TokenType { get; set; }
		//public long UserRegistrationId { get; set; }
	}

	public class CommonMessage
	{
		public CommonMessage(string code, string message)
		{
			Code = code;
			Message = message;
		}
		public string Code { get; set; }
		public string Message { get; set; }
	}


	#region requests

	public class BaseRequest
	{
		public RequestActivity Activity { get; set; }
		public List<CommonMessage> CommonMessages { get; set; }
	}

	public class RequestRegister : BaseRequest
	{
		public RequestRegister()
		{
			//UserName = "test_user";
			//Title = 2147483647;
			//Activity = new RequestActivity();
			//CommonMessages = new List<CommonMessage>();
			//CommonMessages.Add(new CommonMessage("1", "common message"));

			//AltEmail = "test123@mail.com";
			//Email = "test123@test.com";
			//Password = "test1234";
			//FirstName = "first";
			//PrefferedFirstName = "String content";
			//LastName = "last";
			//MiddleName = "middle";
			//SuffixName = "Mr";
			//Gender = 1;

			//DateOfBirth = DateTime.Now;
			//CreatedDate = DateTime.Now;
			//DateOfBirth = DateTime.Now;
			//ActivationExpiryTime = DateTime.Now;

			//CreatedDate = "\\/Date(928129800000+0530)\\/";
			//DateOfBirth = "\\/Date(928129800000+0530)\\/";
			//ActivationExpiryTime = "\\/Date(928129800000+0530)\\/";

			//ActivationKey = "q323123";
			//ActivationState = true;
			//RegistrationId = 0;
			//IsActive = true;
		}

		public string UserName { get; set; }
		public long Title { get; set; }
		public string Email { get; set; }
		public string AltEmail { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string PrefferedFirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string SuffixName { get; set; }
		public long Gender { get; set; }

		//public Nullable<DateTime> DateOfBirth { get; set; }
		//public Nullable<DateTime> ModifedDate { get; set; }
		//public Nullable<DateTime> CreatedDate { get; set; }
		//public Nullable<DateTime> ActivationExpiryTime { get; set; }

		//public string DateOfBirth { get; set; }
		//public string ModifedDate { get; set; }
		//public string CreatedDate { get; set; }
		//public string ActivationExpiryTime { get; set; }


		public string ActivationKey { get; set; }
		public bool ActivationState { get; set; }
		public long RegistrationId { get; set; }
		public bool IsActive { get; set; }

		public bool IsValidate()
		{
			if (string.IsNullOrEmpty(FirstName) ||
				string.IsNullOrEmpty(LastName) ||
				string.IsNullOrEmpty(Email) ||
			    string.IsNullOrEmpty(Password))
				return false;
			
			return true;
		}
	}

	public class RequestLogin : BaseRequest
	{
		public RequestLogin()
		{
			//Email = "test123@test.com";
			//Password = "test1234";
		}

		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsValidate()
		{
			if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
				return false;

			return true;
		}
	}
	#endregion
}
