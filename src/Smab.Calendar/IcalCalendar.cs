using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Calendar
{
	/// <summary>
	/// Class to create iCalendar formatted objects
	/// Implements a subset of RFC 5545 https://tools.ietf.org/html/rfc5545
	/// </summary>
	public class IcalCalendar
    {
		public string ProductId { get; set; } = "-//smab/iCalendar 2.0//EN";
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		/// <summary>
		/// Time To Live in minutes (publication schedule)
		/// Defaults to 1440 minutes (1 day)
		/// </summary>
		public int TimeToLive { get; set; } = 1440;
		public List<VEvent> Events { get; set; } = new List<VEvent>();

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			result.Append("BEGIN:VCALENDAR");
			result.Append(Constants.CrLf);

			result.Append("PRODID:");
			result.Append(ProductId);
			result.Append(Constants.CrLf);

			//The following two lines seem to be required by Outlook to get the alarm settings (Version and Method)
			result.Append("VERSION:2.0");
			result.Append(Constants.CrLf);
			result.Append("METHOD:PUBLISH");
			result.Append(Constants.CrLf);
			
			result.Append("X-WR-CALNAME:");
			result.Append(Name);
			result.Append(Constants.CrLf);

			result.Append("X-WR-CALDESC:");
			result.Append(Description);
			result.Append(Constants.CrLf);
			
			result.Append("X-PUBLISHED-TTL:PT");
			result.Append(TimeToLive.ToString());
			result.Append("M");
			result.Append(Constants.CrLf);

			foreach (var evnt in Events)
			{
				result.Append(evnt.ToString());
			}
			result.Append("END:VCALENDAR");

			return result.ToString();
		}

		public IcalCalendar() { }
		public IcalCalendar(VEvent vEvent) 
		{
			Events.Add(vEvent);
		}
	}
}
