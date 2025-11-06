namespace Smab.Calendar;

/// <summary>
/// Class to create iCalendar formatted objects
/// Implements a subset of RFC 5545 https://tools.ietf.org/html/rfc5545
/// </summary>
public class IcalCalendar
{
	public string  ProductId   { get; set; } = "-//smab/iCalendar 2.0//EN";
	public string  Name        { get; set; } = "";
	public string  Description { get; set; } = "";
	/// <summary>
	/// Time To Live in minutes (publication schedule)
	/// Defaults to 1440 minutes (1 day)
	/// </summary>
	public int      TimeToLive { get; set; } = 1440;
	public List<VEvent> Events { get; set; } = [];

	public override string ToString()
	{
		StringBuilder result = new();

		_ = result.Append("BEGIN:VCALENDAR");
		_ = result.Append(CrLf);

		_ = result.Append("PRODID:");
		_ = result.Append(ProductId);
		_ = result.Append(CrLf);

		//The following two lines seem to be required by Outlook to get the alarm settings (Version and Method)
		_ = result.Append("VERSION:2.0");
		_ = result.Append(CrLf);
		_ = result.Append("METHOD:PUBLISH");
		_ = result.Append(CrLf);

		_ = result.Append("X-WR-CALNAME:");
		_ = result.Append(Name);
		_ = result.Append(CrLf);

		_ = result.Append("X-WR-CALDESC:");
		_ = result.Append(Description);
		_ = result.Append(CrLf);

		_ = result.Append("X-PUBLISHED-TTL:PT");
		_ = result.Append(TimeToLive);
		_ = result.Append('M');
		_ = result.Append(CrLf);

		foreach (var evnt in Events) {
			_ = result.Append(evnt.ToString());
		}

		_ = result.Append("END:VCALENDAR");

		return result.ToString();
	}

	public IcalCalendar() { }
	public IcalCalendar(VEvent vEvent)
	{
		Events.Add(vEvent);
	}
}
