using System;
namespace ParkimonGo
{
	public class RequestActivity
	{
		public RequestActivity()
		{
			ActivityId = 1;
			ActivityName = "Test Activity";
			CustomerId = 0;
			Email = null;
			Token = null;
			TokenType = 0;
			UserRegistrationId = 0;
		}

		public int ActivityId { get; set; }
		public string ActivityName { get; set; }
		public int CustomerId { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
		public int TokenType { get; set; }
		public int UserRegistrationId { get; set; }
	}

	public class RequestRegister
	{
		public RequestRegister()
		{
			Title = 100;
			Activity = new RequestActivity();
			AltEmail = "altmail@mail.com";
			Email = "mail@mail.com";
			Password = "test";
			FirstName = "first";
			PrefferedFirstName = "pname";
			LastName = "last";
			MiddleName = "middle";
			SuffixName = "suffix";
			Gender = 1;
			DateOfBirth = DateTime.Now;
			CreatedDate = DateTime.Now;
			DateOfBirth = DateTime.Now;
			RegistrationId = 0;
			IsActive = true;
		}

		public RequestActivity Activity { get; set; }
		public int Title { get; set; }
		public string Email { get; set; }
		public string AltEmail { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string PrefferedFirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string SuffixName { get; set; }
		public int Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime ModifedDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public int RegistrationId { get; set; }
		public bool IsActive { get; set; }
	}
}
