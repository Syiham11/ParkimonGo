using System;
namespace ParkimonGo
{
	public class Parking
	{
		public Parking(string period, string fees)
		{
			this.period = period;
			this.fees = fees;
		}
		public string period { get; set; }
		public string fees { get; set; }
	}
}
