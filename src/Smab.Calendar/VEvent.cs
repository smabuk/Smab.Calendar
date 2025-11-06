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

	public List<VAlarm> Alarms { get; set; } = [];

	public override string ToString()
	{
		StringBuilder result = new();

		_ = result.Append("BEGIN:VEVENT");
		_ = result.Append(CrLf);

		_ = result.Append("UID:");
		_ = result.Append(UID);
		_ = result.Append(CrLf);

		_ = result.Append("TITLE:");
		_ = result.Append(Summary);
		_ = result.Append(CrLf);

		_ = result.Append("SUMMARY:");
		_ = result.Append(Summary);
		_ = result.Append(CrLf);

		_ = result.Append("PRIORITY:");
		_ = result.Append((int)Priority);
		_ = result.Append(CrLf);

		if (!string.IsNullOrWhiteSpace(Organizer)) {
			_ = result.Append("ORGANIZER:");
			_ = result.Append(Organizer);
			_ = result.Append(CrLf);
		}

		_ = result.Append("TRANSP:");
		_ = result.Append(Transparency.ToString());
		_ = result.Append(CrLf);

		_ = result.Append("X-MICROSOFT-CDO-BUSYSTATUS:");
		_ = result.Append(OutlookBusyStatus.ToString());
		_ = result.Append(CrLf);

		_ = result.Append("LOCATION:");
		_ = result.Append(Location);
		_ = result.Append(CrLf);

		//result.Append("X-MICROSOFT-CDO-ALLDAYEVENT:");
		//result.Append(Constants.CrLf);

		_ = result.Append("DTSTART:");
		_ = result.Append(DateStart.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"));
		_ = result.Append(CrLf);

		_ = result.Append("DTEND:");
		_ = result.Append(DateEnd.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"));
		_ = result.Append(CrLf);

		_ = result.Append("DTSTAMP:");
		_ = result.Append(TimeStamp.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"));
		_ = result.Append(CrLf);

		_ = result.Append("DESCRIPTION:");
		_ = result.Append(Description);
		_ = result.Append(CrLf);

		if (!string.IsNullOrWhiteSpace(Categories)) {
			_ = result.Append("CATEGORIES:");
			_ = result.Append(Categories);
			_ = result.Append(CrLf);
		}

		if (!string.IsNullOrWhiteSpace(URL)) {
			_ = result.Append("URL:");
			_ = result.Append(URL);
			_ = result.Append(CrLf);
		}

		foreach (var alarm in Alarms) {
			_ = result.Append(alarm.ToString());
		}

		_ = result.Append("END:VEVENT");
		_ = result.Append(CrLf);

		return result.ToString();
	}
}
