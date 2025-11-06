namespace Smab.Calendar.Tests;

public class IcalCalendarTests
{
	[Fact]
	public void Constructor_Default_CreatesEmptyCalendar()
	{
		// Arrange & Act
		IcalCalendar calendar = new();

		// Assert
		Assert.Equal("-//smab/iCalendar 2.0//EN", calendar.ProductId);
		Assert.Equal("", calendar.Name);
		Assert.Equal("", calendar.Description);
		Assert.Equal(1440, calendar.TimeToLive);
		Assert.Empty(calendar.Events);
	}

	[Fact]
	public void Constructor_WithVEvent_AddsEventToCollection()
	{
		// Arrange
		VEvent vEvent = new()
		{
			UID = "test-event",
			Summary = "Test Event"
		};

		// Act
		IcalCalendar calendar = new(vEvent);

		// Assert
		_ = Assert.Single(calendar.Events);
		Assert.Equal(vEvent, calendar.Events[0]);
	}

	[Fact]
	public void ToString_EmptyCalendar_ReturnsMinimalICalFormat()
	{
		// Arrange
		IcalCalendar calendar = new();

		string expected = """
			BEGIN:VCALENDAR
			PRODID:-//smab/iCalendar 2.0//EN
			VERSION:2.0
			METHOD:PUBLISH
			X-WR-CALNAME:
			X-WR-CALDESC:
			X-PUBLISHED-TTL:PT1440M
			END:VCALENDAR
			""";

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ToString_WithProperties_ReturnsFormattedICalWithProperties()
	{
		// Arrange
		IcalCalendar calendar = new()
		{
			ProductId = "-//Custom Product//EN",
			Name = "My Calendar",
			Description = "Test Description",
			TimeToLive = 720
		};

		string expected = """
			BEGIN:VCALENDAR
			PRODID:-//Custom Product//EN
			VERSION:2.0
			METHOD:PUBLISH
			X-WR-CALNAME:My Calendar
			X-WR-CALDESC:Test Description
			X-PUBLISHED-TTL:PT720M
			END:VCALENDAR
			""";

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ToString_WithSingleEvent_IncludesEventInOutput()
	{
		// Arrange
		VEvent vEvent = new()
		{
			UID = "event-123",
			Summary = "Test Event",
			DateStart = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
			DateEnd = new DateTime(2024, 1, 15, 11, 0, 0, DateTimeKind.Utc)
		};

		IcalCalendar calendar = new(vEvent)
		{
			Name = "Test Calendar"
		};

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains("BEGIN:VCALENDAR", actual);
		Assert.Contains("X-WR-CALNAME:Test Calendar", actual);
		Assert.Contains("BEGIN:VEVENT", actual);
		Assert.Contains("UID:event-123", actual);
		Assert.Contains("SUMMARY:Test Event", actual);
		Assert.Contains("END:VEVENT", actual);
		Assert.Contains("END:VCALENDAR", actual);
	}

	[Fact]
	public void ToString_WithMultipleEvents_IncludesAllEvents()
	{
		// Arrange
		VEvent event1 = new()
		{
			UID = "event-1",
			Summary = "First Event"
		};

		VEvent event2 = new()
		{
			UID = "event-2",
			Summary = "Second Event"
		};

		VEvent event3 = new()
		{
			UID = "event-3",
			Summary = "Third Event"
		};

		IcalCalendar calendar = new()
		{
			Name = "Multi-Event Calendar",
			Events = [event1, event2, event3]
		};

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains("BEGIN:VCALENDAR", actual);
		Assert.Contains("UID:event-1", actual);
		Assert.Contains("SUMMARY:First Event", actual);
		Assert.Contains("UID:event-2", actual);
		Assert.Contains("SUMMARY:Second Event", actual);
		Assert.Contains("UID:event-3", actual);
		Assert.Contains("SUMMARY:Third Event", actual);
		Assert.Contains("END:VCALENDAR", actual);
	}

	[Fact]
	public void ToString_OutputFormat_ContainsRequiredICalComponents()
	{
		// Arrange
		IcalCalendar calendar = new();

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains("BEGIN:VCALENDAR", actual);
		Assert.Contains("VERSION:2.0", actual);
		Assert.Contains("METHOD:PUBLISH", actual);
		Assert.Contains("END:VCALENDAR", actual);
		Assert.StartsWith("BEGIN:VCALENDAR", actual);
		Assert.EndsWith("END:VCALENDAR", actual);
	}

	[Fact]
	public void ToString_OutputFormat_UsesCorrectLineBreaks()
	{
		// Arrange
		IcalCalendar calendar = new();

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains("\r\n", actual);
		string[] lines = actual.Split("\r\n");
		Assert.True(lines.Length > 1, "Output should contain multiple lines");
	}

	[Fact]
	public void ProductId_SetAndGet_ReturnsCorrectValue()
	{
		// Arrange
		IcalCalendar calendar = new();
		string productId = "-//My Organization//Product Name//EN";

		// Act
		calendar.ProductId = productId;

		// Assert
		Assert.Equal(productId, calendar.ProductId);
	}

	[Fact]
	public void Name_SetAndGet_ReturnsCorrectValue()
	{
		// Arrange
		IcalCalendar calendar = new();
		string name = "My Custom Calendar Name";

		// Act
		calendar.Name = name;

		// Assert
		Assert.Equal(name, calendar.Name);
	}

	[Fact]
	public void Description_SetAndGet_ReturnsCorrectValue()
	{
		// Arrange
		IcalCalendar calendar = new();
		string description = "This is a detailed calendar description";

		// Act
		calendar.Description = description;

		// Assert
		Assert.Equal(description, calendar.Description);
	}

	[Fact]
	public void TimeToLive_SetAndGet_ReturnsCorrectValue()
	{
		// Arrange
		IcalCalendar calendar = new();
		int ttl = 60;

		// Act
		calendar.TimeToLive = ttl;

		// Assert
		Assert.Equal(ttl, calendar.TimeToLive);
	}

	[Fact]
	public void TimeToLive_DefaultValue_Is1440Minutes()
	{
		// Arrange & Act
		IcalCalendar calendar = new();

		// Assert
		Assert.Equal(1440, calendar.TimeToLive);
	}

	[Fact]
	public void Events_AddEvent_IncreasesCount()
	{
		// Arrange
		IcalCalendar calendar = new();
		VEvent vEvent = new() { UID = "new-event" };

		// Act
		calendar.Events.Add(vEvent);

		// Assert
		_ = Assert.Single(calendar.Events);
	}

	[Fact]
	public void Events_RemoveEvent_DecreasesCount()
	{
		// Arrange
		VEvent vEvent = new() { UID = "event-to-remove" };
		IcalCalendar calendar = new(vEvent);

		// Act
		_ = calendar.Events.Remove(vEvent);

		// Assert
		Assert.Empty(calendar.Events);
	}

	[Fact]
	public void ToString_WithComplexCalendar_GeneratesValidICalFormat()
	{
		// Arrange
		VEvent event1 = new()
		{
			UID = Guid.NewGuid().ToString(),
			Summary = "🏸 Badminton Match",
			Location = "Sports Center",
			DateStart = new DateTime(2024, 3, 15, 19, 30, 0, DateTimeKind.Utc),
			DateEnd = new DateTime(2024, 3, 15, 21, 30, 0, DateTimeKind.Utc),
			Priority = VEvent.PriorityLevel.High,
			Categories = "Sports,Badminton",
			Description = "Championship match"
		};

		IcalCalendar calendar = new()
		{
			ProductId = "-//Sports League//Calendar 1.0//EN",
			Name = "Badminton League",
			Description = "Season fixtures and results",
			TimeToLive = 720,
			Events = [event1]
		};

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains("BEGIN:VCALENDAR", actual);
		Assert.Contains("PRODID:-//Sports League//Calendar 1.0//EN", actual);
		Assert.Contains("X-WR-CALNAME:Badminton League", actual);
		Assert.Contains("X-WR-CALDESC:Season fixtures and results", actual);
		Assert.Contains("X-PUBLISHED-TTL:PT720M", actual);
		Assert.Contains("BEGIN:VEVENT", actual);
		Assert.Contains("SUMMARY:🏸 Badminton Match", actual);
		Assert.Contains("LOCATION:Sports Center", actual);
		Assert.Contains("CATEGORIES:Sports,Badminton", actual);
		Assert.Contains("END:VEVENT", actual);
		Assert.Contains("END:VCALENDAR", actual);
	}

	[Theory]
	[InlineData(60)]
	[InlineData(120)]
	[InlineData(720)]
	[InlineData(1440)]
	[InlineData(10080)]
	public void TimeToLive_VariousValues_FormatsCorrectly(int ttl)
	{
		// Arrange
		IcalCalendar calendar = new()
		{
			TimeToLive = ttl
		};

		string expected = $"X-PUBLISHED-TTL:PT{ttl}M";

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains(expected, actual);
	}

	[Fact]
	public void ToString_EmptyEventsList_DoesNotIncludeEventBlocks()
	{
		// Arrange
		IcalCalendar calendar = new()
		{
			Name = "Empty Calendar",
			Events = []
		};

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.DoesNotContain("BEGIN:VEVENT", actual);
		Assert.DoesNotContain("END:VEVENT", actual);
	}

	[Fact]
	public void ToString_SpecialCharactersInName_IncludesInOutput()
	{
		// Arrange
		IcalCalendar calendar = new()
		{
			Name = "Calendar with émojis 🎉 and spëcial chars",
			Description = "Testing spëcial châráctérs"
		};

		// Act
		string actual = calendar.ToString();

		// Assert
		Assert.Contains("X-WR-CALNAME:Calendar with émojis 🎉 and spëcial chars", actual);
		Assert.Contains("X-WR-CALDESC:Testing spëcial châráctérs", actual);
	}
}
