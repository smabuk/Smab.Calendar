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

	[Fact]
	public void UID_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.UID);
	}

	[Fact]
	public void UID_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string uid = "test-uid-123";
		vEvent.UID = uid;
		Assert.Equal(uid, vEvent.UID);
	}

	[Fact]
	public void DateStart_DefaultValue_IsMinValue()
	{
		VEvent vEvent = new();
		Assert.Equal(default, vEvent.DateStart);
	}

	[Fact]
	public void DateStart_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		DateTime date = new(2024, 1, 15, 10, 30, 0);
		vEvent.DateStart = date;
		Assert.Equal(date, vEvent.DateStart);
	}

	[Fact]
	public void DateEnd_DefaultValue_IsMinValue()
	{
		VEvent vEvent = new();
		Assert.Equal(default, vEvent.DateEnd);
	}

	[Fact]
	public void DateEnd_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		DateTime date = new(2024, 1, 15, 12, 30, 0);
		vEvent.DateEnd = date;
		Assert.Equal(date, vEvent.DateEnd);
	}

	[Fact]
	public void TimeStamp_DefaultValue_IsApproximatelyNow()
	{
		DateTime before = DateTime.Now.AddSeconds(-1);
		VEvent vEvent = new();
		DateTime after = DateTime.Now.AddSeconds(1);
		
		Assert.InRange(vEvent.TimeStamp, before, after);
	}

	[Fact]
	public void TimeStamp_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		DateTime timestamp = new(2024, 1, 1, 0, 0, 0);
		vEvent.TimeStamp = timestamp;
		Assert.Equal(timestamp, vEvent.TimeStamp);
	}

	[Fact]
	public void Summary_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.Summary);
	}

	[Fact]
	public void Summary_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string summary = "Test Event";
		vEvent.Summary = summary;
		Assert.Equal(summary, vEvent.Summary);
	}

	[Fact]
	public void Organizer_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.Organizer);
	}

	[Fact]
	public void Organizer_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string organizer = "mailto:organizer@example.com";
		vEvent.Organizer = organizer;
		Assert.Equal(organizer, vEvent.Organizer);
	}

	[Fact]
	public void Location_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.Location);
	}

	[Fact]
	public void Location_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string location = "Conference Room A";
		vEvent.Location = location;
		Assert.Equal(location, vEvent.Location);
	}

	[Fact]
	public void Priority_DefaultValue_IsNormal()
	{
		VEvent vEvent = new();
		Assert.Equal(PriorityLevel.Normal, vEvent.Priority);
	}

	[Theory]
	[InlineData(PriorityLevel.NoPriority)]
	[InlineData(PriorityLevel.High)]
	[InlineData(PriorityLevel.Normal)]
	[InlineData(PriorityLevel.Low)]
	public void Priority_SetValue_ReturnsSetValue(PriorityLevel priority)
	{
		VEvent vEvent = new()
		{
			Priority = priority
		};
		Assert.Equal(priority, vEvent.Priority);
	}

	[Fact]
	public void Description_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.Description);
	}

	[Fact]
	public void Description_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string description = "Test description";
		vEvent.Description = description;
		Assert.Equal(description, vEvent.Description);
	}

	[Fact]
	public void Description_WithCRLF_ConvertedToLiteralBackslashN()
	{
		VEvent vEvent = new()
		{
			Description = "Line1\r\nLine2"
		};
		Assert.Equal(@"Line1\nLine2", vEvent.Description);
	}

	[Fact]
	public void Description_WithLF_ConvertedToLiteralBackslashN()
	{
		VEvent vEvent = new()
		{
			Description = "Line1\nLine2"
		};
		Assert.Equal(@"Line1\nLine2", vEvent.Description);
	}

	[Fact]
	public void Description_WithMultipleNewlines_AllConvertedToLiteralBackslashN()
	{
		VEvent vEvent = new()
		{
			Description = "Line1\r\nLine2\nLine3\r\nLine4"
		};
		Assert.Equal(@"Line1\nLine2\nLine3\nLine4", vEvent.Description);
	}

	[Fact]
	public void Transparency_DefaultValue_IsOpaque()
	{
		VEvent vEvent = new();
		Assert.Equal(TransparencyType.OPAQUE, vEvent.Transparency);
	}

	[Theory]
	[InlineData(TransparencyType.OPAQUE)]
	[InlineData(TransparencyType.TRANSPARENT)]
	public void Transparency_SetValue_ReturnsSetValue(TransparencyType transparency)
	{
		VEvent vEvent = new()
		{
			Transparency = transparency
		};
		Assert.Equal(transparency, vEvent.Transparency);
	}

	[Fact]
	public void OutlookBusyStatus_WhenTransparencyIsOpaque_ReturnsBusy()
	{
		VEvent vEvent = new()
		{
			Transparency = TransparencyType.OPAQUE
		};
		Assert.Equal(OutlookBusyStatusType.BUSY, vEvent.OutlookBusyStatus);
	}

	[Fact]
	public void OutlookBusyStatus_WhenTransparencyIsTransparent_ReturnsFree()
	{
		VEvent vEvent = new()
		{
			Transparency = TransparencyType.TRANSPARENT
		};
		Assert.Equal(OutlookBusyStatusType.FREE, vEvent.OutlookBusyStatus);
	}

	[Fact]
	public void URL_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.URL);
	}

	[Fact]
	public void URL_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string url = "https://example.com/event";
		vEvent.URL = url;
		Assert.Equal(url, vEvent.URL);
	}

	[Fact]
	public void AllDayEvent_DefaultValue_IsFalse()
	{
		VEvent vEvent = new();
		Assert.False(vEvent.AllDayEvent);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void AllDayEvent_SetValue_ReturnsSetValue(bool allDay)
	{
		VEvent vEvent = new()
		{
			AllDayEvent = allDay
		};
		Assert.Equal(allDay, vEvent.AllDayEvent);
	}

	[Fact]
	public void Categories_DefaultValue_IsEmptyString()
	{
		VEvent vEvent = new();
		Assert.Equal("", vEvent.Categories);
	}

	[Fact]
	public void Categories_SetValue_ReturnsSetValue()
	{
		VEvent vEvent = new();
		string categories = "Work,Meeting";
		vEvent.Categories = categories;
		Assert.Equal(categories, vEvent.Categories);
	}

	[Fact]
	public void Alarms_DefaultValue_IsEmptyList()
	{
		VEvent vEvent = new();
		Assert.NotNull(vEvent.Alarms);
		Assert.Empty(vEvent.Alarms);
	}

	[Fact]
	public void Alarms_AddAlarms_AreIncludedInToString()
	{
		VEvent vEvent = new()
		{
			UID = "test",
			DateStart = new DateTime(2024, 1, 1, 10, 0, 0),
			DateEnd = new DateTime(2024, 1, 1, 11, 0, 0),
			Alarms = [
				new VAlarm(TimeSpan.FromMinutes(-15))
			]
		};

		string result = vEvent.ToString();
		Assert.Contains("BEGIN:VALARM", result);
		Assert.Contains("END:VALARM", result);
	}

	[Fact]
	public void ToString_WithOrganizer_IncludesOrganizerLine()
	{
		VEvent vEvent = new()
		{
			Organizer = "mailto:test@example.com"
		};

		string result = vEvent.ToString();
		Assert.Contains("ORGANIZER:mailto:test@example.com", result);
	}

	[Fact]
	public void ToString_WithoutOrganizer_ExcludesOrganizerLine()
	{
		VEvent vEvent = new()
		{
			Organizer = ""
		};

		string result = vEvent.ToString();
		Assert.DoesNotContain("ORGANIZER:", result);
	}

	[Fact]
	public void ToString_WithWhitespaceOrganizer_ExcludesOrganizerLine()
	{
		VEvent vEvent = new()
		{
			Organizer = "   "
		};

		string result = vEvent.ToString();
		Assert.DoesNotContain("ORGANIZER:", result);
	}

	[Fact]
	public void ToString_WithCategories_IncludesCategoriesLine()
	{
		VEvent vEvent = new()
		{
			Categories = "Work,Meeting"
		};

		string result = vEvent.ToString();
		Assert.Contains("CATEGORIES:Work,Meeting", result);
	}

	[Fact]
	public void ToString_WithoutCategories_ExcludesCategoriesLine()
	{
		VEvent vEvent = new()
		{
			Categories = ""
		};

		string result = vEvent.ToString();
		Assert.DoesNotContain("CATEGORIES:", result);
	}

	[Fact]
	public void ToString_WithURL_IncludesURLLine()
	{
		VEvent vEvent = new()
		{
			URL = "https://example.com"
		};

		string result = vEvent.ToString();
		Assert.Contains("URL:https://example.com", result);
	}

	[Fact]
	public void ToString_WithoutURL_ExcludesURLLine()
	{
		VEvent vEvent = new()
		{
			URL = ""
		};

		string result = vEvent.ToString();
		Assert.DoesNotContain("URL:", result);
	}

	[Fact]
	public void ToString_DateTimes_ConvertedToUTC()
	{
		VEvent vEvent = new()
		{
			DateStart = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Local),
			DateEnd = new DateTime(2024, 1, 15, 11, 0, 0, DateTimeKind.Local),
			TimeStamp = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Local)
		};

		string result = vEvent.ToString();
		
		// All dates should end with Z indicating UTC
		Assert.Contains("DTSTART:", result);
		Assert.Contains("Z", result.Split("DTSTART:")[1].Split('\n')[0]);
		Assert.Contains("DTEND:", result);
		Assert.Contains("Z", result.Split("DTEND:")[1].Split('\n')[0]);
		Assert.Contains("DTSTAMP:", result);
		Assert.Contains("Z", result.Split("DTSTAMP:")[1].Split('\n')[0]);
	}

	[Fact]
	public void ToString_PriorityValue_OutputAsInteger()
	{
		VEvent vEvent = new()
		{
			Priority = PriorityLevel.High
		};

		string result = vEvent.ToString();
		Assert.Contains("PRIORITY:1", result);
	}

	[Fact]
	public void ToString_ContainsBeginAndEndTags()
	{
		VEvent vEvent = new();
		string result = vEvent.ToString();
		
		Assert.StartsWith("BEGIN:VEVENT", result);
		Assert.Contains("END:VEVENT", result);
	}

	[Fact]
	public void ToString_IncludesBothTitleAndSummary()
	{
		VEvent vEvent = new()
		{
			Summary = "Test Event"
		};

		string result = vEvent.ToString();
		Assert.Contains("TITLE:Test Event", result);
		Assert.Contains("SUMMARY:Test Event", result);
	}

	[Fact]
	public void ToString_MultipleAlarms_AllIncluded()
	{
		VEvent vEvent = new()
		{
			UID = "test",
			DateStart = new DateTime(2024, 1, 1, 10, 0, 0),
			DateEnd = new DateTime(2024, 1, 1, 11, 0, 0),
			Alarms = [
				new VAlarm(TimeSpan.FromMinutes(-15)),
				new VAlarm(TimeSpan.FromHours(-1))
			]
		};

		string result = vEvent.ToString();
		int alarmCount = result.Split("BEGIN:VALARM").Length - 1;
		Assert.Equal(2, alarmCount);
	}
}
