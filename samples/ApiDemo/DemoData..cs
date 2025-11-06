namespace ApiDemo;

public static class DemoData
{
	public static IcalCalendar GetDemoCalendar(string name)
	{
		IcalCalendar ical = new()
		{
			Name = $"{name}",
			Description = "Fixtures and results of matches for the Badminton League"
		};

		VEvent fixtureEvent = new()
		{
			UID = $"RBL Home Team vs Away Team",
			Summary = $"🏸 Home Team vs Away Team",
			Location = $"Venue",
			DateStart = DateTime.Now.AddDays(1).Date.AddHours(19).AddMinutes(30),
			DateEnd = DateTime.Now.AddDays(1).Date.AddHours(22).AddMinutes(0),
			Priority = VEvent.PriorityLevel.Normal,
			Transparency = VEvent.TransparencyType.TRANSPARENT,
			Categories = "Badminton,Completed",
			Description = @"\nSCORE: 3-2\nHome team WINS\n",
		};
		ical.Events.Add(fixtureEvent);

		fixtureEvent = new VEvent
		{
			UID = $"Away Team vs Heme Team",
			Summary = $"🏸 Home Team vs Away Team",
			Location = $"Other Venue",
			DateStart = DateTime.Now.AddDays(8).Date.AddHours(19).AddMinutes(30),
			DateEnd = DateTime.Now.AddDays(8).Date.AddHours(22).AddMinutes(0),
			Priority = VEvent.PriorityLevel.Normal,
			Transparency = VEvent.TransparencyType.OPAQUE,
			Alarms =
				[
					new() {
						Trigger     = new TimeSpan(0, 0, 60, 0),
						Action      = VAlarm.ActionType.DISPLAY,
						Description = "Reminder"
					}
				],
			Categories = "Badminton",
			Description = @"\n",
		};

		ical.Events.Add(fixtureEvent);

		return ical;
	}
}
