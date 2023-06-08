namespace Smab.Calendar;

public partial class VEvent
{
	private string _description = "";

	/// <summary>
	/// Unique identifier for the event
	/// </summary>
	public string UID { get; set; } = "";

	/// <summary>
	/// Start date of the event.
	/// Will be automatically converted to UTC
	/// </summary>
	public DateTime DateStart { get; set; }

	/// <summary>
	/// End date of the event.
	/// Will be automatically converted to UTC
	/// </summary>
	public DateTime DateEnd { get; set; }

	/// <summary>
	/// Timestamp.
	/// Will be automatically converted to UTC
	/// </summary>
	public DateTime TimeStamp { get; set; } = DateTime.Now;

	/// <summary>
	/// Summary/Subject of the event
	/// </summary>
	public string Summary { get; set; } = "";

	/// <summary>
	/// Can be 
	///    mailto:
	///    url
	///    just a name
	/// </summary>
	public string Organizer { get; set; } = "";

	public string Location { get; set; } = "";

	public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

	// For some reason sometimes the string sequence \n is getting converted to vbLf
	// This may be because of ASP.Net Core 2.0 or it may be c# to vb string passing
	// Either way - this is a workaround to put the \n literal sequence back into the string
	public string Description {
		get => _description;
		set => _description = value.Replace("\r\n", @"\n").Replace("\n", @"\n");
	}

	public TransparencyType Transparency { get; set; } = TransparencyType.OPAQUE;
	public OutlookBusyStatusType OutlookBusyStatus
	{
		get => Transparency switch
		{
			TransparencyType.OPAQUE      => OutlookBusyStatusType.BUSY,
			TransparencyType.TRANSPARENT => OutlookBusyStatusType.FREE,
			_ => OutlookBusyStatusType.FREE
		};

		private set { }
	}

	public string URL { get; set; } = "";

	/// <summary>
	/// If true, then this event lasts all day
	/// Defaults to false
	/// </summary>
	public bool AllDayEvent { get; set; } = false;

	/// <summary>
	/// Categories delimited by commas
	/// </summary>
	public string Categories { get; set; } = "";

	public List<VAlarm> Alarms { get; set; } = new List<VAlarm>();

	public override string ToString()
	{
		StringBuilder result = new StringBuilder();

		result.Append("BEGIN:VEVENT");
		result.Append(Constants.CrLf);

		result.Append("UID:");
		result.Append(UID);
		result.Append(Constants.CrLf);

		result.Append("TITLE:");
		result.Append(Summary);
		result.Append(Constants.CrLf);

		result.Append("SUMMARY:");
		result.Append(Summary);
		result.Append(Constants.CrLf);

		result.Append("PRIORITY:");
		result.Append((int)Priority);
		result.Append(Constants.CrLf);

		if (!string.IsNullOrWhiteSpace(Organizer)) {
			result.Append("ORGANIZER:");
			result.Append(Organizer);
			result.Append(Constants.CrLf);
		}

		result.Append("TRANSP:");
		result.Append(Transparency.ToString());
		result.Append(Constants.CrLf);

		result.Append("X-MICROSOFT-CDO-BUSYSTATUS:");
		result.Append(OutlookBusyStatus.ToString());
		result.Append(Constants.CrLf);

		result.Append("LOCATION:");
		result.Append(Location);
		result.Append(Constants.CrLf);

		//result.Append("X-MICROSOFT-CDO-ALLDAYEVENT:");
		//result.Append(Constants.CrLf);

		result.Append("DTSTART:");
		result.Append(DateStart.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"));
		result.Append(Constants.CrLf);

		result.Append("DTEND:");
		result.Append(DateEnd.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"));
		result.Append(Constants.CrLf);

		result.Append("DTSTAMP:");
		result.Append(TimeStamp.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"));
		result.Append(Constants.CrLf);

		result.Append("DESCRIPTION:");
		result.Append(Description);
		result.Append(Constants.CrLf);

		if (!string.IsNullOrWhiteSpace(Categories)) {
			result.Append("CATEGORIES:");
			result.Append(Categories);
			result.Append(Constants.CrLf);
		}

		if (!string.IsNullOrWhiteSpace(URL)) {
			result.Append("URL:");
			result.Append(URL);
			result.Append(Constants.CrLf);
		}

		foreach (var alarm in Alarms) {
			result.Append(alarm.ToString());
		}

		result.Append("END:VEVENT");
		result.Append(Constants.CrLf);

		return result.ToString();
	}
}
