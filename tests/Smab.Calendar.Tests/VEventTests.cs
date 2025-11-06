using static Smab.Calendar.VEvent;

namespace Smab.Calendar.Tests;

public class VEventTests
{

	[Fact]
	public void Empty_ReturnsExpected()
	{
		string expected = """
			BEGIN:VEVENT
			UID:
			TITLE:
			SUMMARY:
			PRIORITY:5
			TRANSP:OPAQUE
			X-MICROSOFT-CDO-BUSYSTATUS:BUSY
			LOCATION:
			DTSTART:00010101T000000Z
			DTEND:00010101T000000Z
			DTSTAMP:xxxxxxxxTxxxxxxZ
			DESCRIPTION:
			END:VEVENT

			""";
		string[] expectedArray = expected.Split(Environment.NewLine);

		VEvent vEvent = new();
		string actual = vEvent.ToString();
		string[] actualArray = actual.Split(Environment.NewLine);
		
		for (int i = 0; i < actualArray.Length; i++) {
			string line = actualArray[i];
			if (line.StartsWith("DTSTAMP")) {
				continue;
			}

			Assert.Equal(expectedArray[i], actualArray[i]);
		}
	}

	[Fact]
	public void VEvent_WithDetails_ReturnsExpected()
	{
		Guid guid = Guid.NewGuid();
		string summary = "🏸 Home Team vs Away Team";
		string venue = "Venue";
		DateTime dateStart = new(2022, 11, 10, 19, 30, 00);
		DateTime dateEnd = dateStart.AddHours(3);
		string categories = "Badminton,Completed";
		string description = """
			
			SCORE: 3-2
			Home team WINS

			""";

	string[] expected = $"""
			BEGIN:VEVENT
			UID:{guid}
			TITLE:{summary}
			SUMMARY:{summary}
			PRIORITY:1
			TRANSP:TRANSPARENT
			X-MICROSOFT-CDO-BUSYSTATUS:FREE
			LOCATION:{venue}
			DTSTART:20221110T193000Z
			DTEND:20221110T223000Z
			DTSTAMP:xxxxxxxxTxxxxxxZ
			DESCRIPTION:\nSCORE: 3-2\nHome team WINS\n
			CATEGORIES:{categories}
			END:VEVENT

			"""
			.Split(Environment.NewLine);

		VEvent vEvent = new()
		{
			UID = guid.ToString(),
			Summary = summary,
			Location = venue,
			DateStart = dateStart,
			DateEnd   = dateEnd,
			Priority  = PriorityLevel.High,
			Transparency = TransparencyType.TRANSPARENT,
			Categories = categories,
			Description = description,
		};

		string[] actual = vEvent
			.ToString()
			.Split(Environment.NewLine);
		
		for (int i = 0; i < actual.Length; i++) {
			string line = actual[i];
			if (line.StartsWith("DTSTAMP")) {
				continue;
			}

			Assert.Equal(expected[i], actual[i]);
		}
	}
}
