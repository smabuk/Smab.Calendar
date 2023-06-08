namespace ApiDemo.EndPoints;

public static partial class CalendarEndPoints
{
	public static void MapCalendarEndPoints(this WebApplication? app)
	{
		app?.MapGet("/calendar/{LeagueName}", GetCalendarByTeam);
	}

	public static IResult GetCalendarByTeam(string leagueName, string? command, HttpContext context)
	{
		IcalCalendar ical = DemoData.GetDemoCalendar(leagueName);

		return command?.ToUpperInvariant() switch
		{
			"TEXT" => Results.Content(ical.ToString(), "text/plain"),
			"FILE" => Results.File(System.Text.Encoding.UTF8.GetBytes(ical.ToString()), "text/calendar", $"{leagueName} Fixtures.ics"),
			"JSON" => Results.Json(ical),
			_      => context.Request.Headers.Accept.FirstOrDefault() switch // simplistic check for accepted return types
				{
					"application/json" => Results.Json(ical),
					"text/calendar" => Results.File(System.Text.Encoding.UTF8.GetBytes(ical.ToString()), "text/calendar", $"{leagueName} Fixtures.ics"),
					"text/plain" => Results.Content(ical.ToString(), "text/plain"),
					_ => Results.File(System.Text.Encoding.UTF8.GetBytes(ical.ToString()), "text/calendar", $"{leagueName} Fixtures.ics"),
				},
		};
	}

}
