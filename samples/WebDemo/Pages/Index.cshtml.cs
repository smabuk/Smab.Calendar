using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Smab.Calendar;

namespace WebDemo.Pages
{
	public class IndexModel : PageModel
	{

		public string PreformattedIcal { get; set; } = "";

		public IndexModel()
		{
		}

		public void OnGet()
		{
			IcalCalendar ical = new IcalCalendar
			{
				Name = $"Badminton League",
				Description = "Fixtures and results of matches for the Badminton League"
			};

			VEvent fixtureEvent = new VEvent
			{
				UID = $"RBL Home Team vs Away Team",
				Summary = $"🏸 Home Team vs Away Team",
				Location = $"Venue",
				DateStart = DateTime.Now.AddDays(1).Date.AddHours(19).AddMinutes(30),
				DateEnd = DateTime.Now.AddDays(1).Date.AddHours(22).AddMinutes(0),
				Priority = VEvent.PriorityLevel.Normal,
				Transparency = VEvent.TransparencyType.TRANSPARENT,
				Categories = "Badminton,Completed",
				Description = @"\nSCORE: 3-2\nHome team WINS\n"
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
				Alarms = new List<VAlarm>
					{
						new VAlarm
						{
							Trigger = new TimeSpan(0, 0, 60, 0),
							Action = VAlarm.ActionType.DISPLAY,
							Description = "Reminder"
						}
					},
				Categories = "Badminton",
				Description = @"\n"
			};

			ical.Events.Add(fixtureEvent);

			PreformattedIcal = ical.ToString();

		}

	}
}

