namespace Smab.Calendar;

public partial class VAlarm
{
	/// <summary>
	/// Amount of time before the event to start displaying the alarm
	/// </summary>
	public TimeSpan Trigger { get; set; } = TimeSpan.FromDays(1);

	/// <summary>
	/// Action to take to notify the user of the alarm
	/// </summary>
	public ActionType Action { get; set; } = ActionType.DISPLAY;

	/// <summary>
	/// Description to display
	/// </summary>
	public string Description { get; set; } = "Reminder";

	public VAlarm() { }

	public VAlarm(TimeSpan trigger)
	{
		Trigger = trigger;
	}

	public VAlarm(TimeSpan trigger, ActionType action, string description)
	{
		Trigger = trigger;
		Action = action;
		Description = description;
	}

	public override string ToString()
	{
		StringBuilder result = new();

		_ = result.Append("BEGIN:VALARM");
		_ = result.Append(CrLf);

		_ = result.Append("TRIGGER:-P");
		_ = result.Append('T');
		_ = result.Append(Trigger.Days);
		_ = result.Append('D');
		_ = result.Append(Trigger.Hours);
		_ = result.Append('H');
		_ = result.Append(Trigger.Minutes);
		_ = result.Append('M');
		_ = result.Append(CrLf);

		_ = result.Append("ACTION:");
		_ = result.Append(Action.ToString());
		_ = result.Append(CrLf);

		_ = result.Append("DESCRIPTION:");
		_ = result.Append(Description);
		_ = result.Append(CrLf);

		_ = result.Append("END:VALARM");
		_ = result.Append(CrLf);

		return result.ToString();
	}
}
