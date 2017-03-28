using System;
namespace ParkimonGo
{
	public class Payment
	{
		public Payment(string no, string description, int id, DateTime date)
		{
			this.no = no;
			this.description = description;
			this.id = id;
			this.date = date;
		}
		public string no { get; set; }
		public string description { get; set; }
		public int id { get; set; }
		public DateTime date { get; set; }
	}
}
