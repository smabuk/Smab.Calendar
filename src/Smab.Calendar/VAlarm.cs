using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Calendar
{
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
			StringBuilder result = new StringBuilder();

			result.Append("BEGIN:VALARM");
			result.Append(Constants.CrLf);

			result.Append("TRIGGER:-P");
			result.Append("T");
			result.Append(Trigger.Days);
			result.Append("D");
			result.Append(Trigger.Hours);
			result.Append("H");
			result.Append(Trigger.Minutes);
			result.Append("M");
			result.Append(Constants.CrLf);

			result.Append("ACTION:");
			result.Append(Action.ToString());
			result.Append(Constants.CrLf);

			result.Append("DESCRIPTION:");
			result.Append(Description);
			result.Append(Constants.CrLf);

			result.Append("END:VALARM");
			result.Append(Constants.CrLf);

			return result.ToString();
		}
	}
}