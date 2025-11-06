using static Smab.Calendar.VAlarm;

namespace Smab.Calendar.Tests;

public class VAlarmTests
{
	[Fact]
	public void DefaultConstructor_SetsDefaultValues()
	{
		VAlarm alarm = new();

		Assert.Equal(TimeSpan.FromDays(1), alarm.Trigger);
		Assert.Equal(ActionType.DISPLAY, alarm.Action);
		Assert.Equal("Reminder", alarm.Description);
	}

	[Fact]
	public void ConstructorWithTrigger_SetsTrigger()
	{
		TimeSpan trigger = TimeSpan.FromMinutes(-15);
		
		VAlarm alarm = new(trigger);

		Assert.Equal(trigger, alarm.Trigger);
		Assert.Equal(ActionType.DISPLAY, alarm.Action);
		Assert.Equal("Reminder", alarm.Description);
	}

	[Fact]
	public void ConstructorWithAllParameters_SetsAllProperties()
	{
		TimeSpan trigger = TimeSpan.FromHours(-2);
		ActionType action = ActionType.EMAIL;
		string description = "Custom reminder";

		VAlarm alarm = new(trigger, action, description);

		Assert.Equal(trigger, alarm.Trigger);
		Assert.Equal(action, alarm.Action);
		Assert.Equal(description, alarm.Description);
	}

	[Fact]
	public void Trigger_DefaultValue_IsOneDayBefore()
	{
		VAlarm alarm = new();
		Assert.Equal(TimeSpan.FromDays(1), alarm.Trigger);
	}

	[Fact]
	public void Trigger_SetValue_ReturnsSetValue()
	{
		VAlarm alarm = new();
		TimeSpan trigger = TimeSpan.FromMinutes(-30);
		alarm.Trigger = trigger;
		Assert.Equal(trigger, alarm.Trigger);
	}

	[Fact]
	public void Action_DefaultValue_IsDisplay()
	{
		VAlarm alarm = new();
		Assert.Equal(ActionType.DISPLAY, alarm.Action);
	}

	[Fact]
	public void Action_SetValue_Display_ReturnsDisplay()
	{
		VAlarm alarm = new()
		{
			Action = ActionType.DISPLAY
		};
		Assert.Equal(ActionType.DISPLAY, alarm.Action);
	}

	[Fact]
	public void Action_SetValue_Audio_ReturnsAudio()
	{
		VAlarm alarm = new()
		{
			Action = ActionType.AUDIO
		};
		Assert.Equal(ActionType.AUDIO, alarm.Action);
	}

	[Fact]
	public void Action_SetValue_Email_ReturnsEmail()
	{
		VAlarm alarm = new()
		{
			Action = ActionType.EMAIL
		};
		Assert.Equal(ActionType.EMAIL, alarm.Action);
	}

	[Fact]
	public void Description_DefaultValue_IsReminder()
	{
		VAlarm alarm = new();
		Assert.Equal("Reminder", alarm.Description);
	}

	[Fact]
	public void Description_SetValue_ReturnsSetValue()
	{
		VAlarm alarm = new();
		string description = "Important meeting";
		alarm.Description = description;
		Assert.Equal(description, alarm.Description);
	}

	[Fact]
	public void ToString_DefaultAlarm_ReturnsExpectedFormat()
	{
		string expected = """
			BEGIN:VALARM
			TRIGGER:-PT1D0H0M
			ACTION:DISPLAY
			DESCRIPTION:Reminder
			END:VALARM

			""";

		VAlarm alarm = new();
		string actual = alarm.ToString();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ToString_ContainsBeginAndEndTags()
	{
		VAlarm alarm = new();
		string result = alarm.ToString();

		Assert.StartsWith("BEGIN:VALARM", result);
		Assert.Contains("END:VALARM", result);
	}

	[Fact]
	public void ToString_MinutesTrigger_FormatsCorrectly()
	{
		VAlarm alarm = new(TimeSpan.FromMinutes(-15));
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:-PT0D0H15M", result);
	}

	[Fact]
	public void ToString_HoursTrigger_FormatsCorrectly()
	{
		VAlarm alarm = new(TimeSpan.FromHours(-2));
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:-PT0D2H0M", result);
	}

	[Fact]
	public void ToString_DaysTrigger_FormatsCorrectly()
	{
		VAlarm alarm = new(TimeSpan.FromDays(-3));
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:-PT3D0H0M", result);
	}

	[Fact]
	public void ToString_ComplexTrigger_FormatsCorrectly()
	{
		TimeSpan trigger = new(2, 3, 45, 0);
		VAlarm alarm = new(trigger);
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:-PT2D3H45M", result);
	}

	[Fact]
	public void ToString_ActionDisplay_IncludesCorrectAction()
	{
		VAlarm alarm = new()
		{
			Action = ActionType.DISPLAY
		};
		string result = alarm.ToString();

		Assert.Contains("ACTION:DISPLAY", result);
	}

	[Fact]
	public void ToString_ActionAudio_IncludesCorrectAction()
	{
		VAlarm alarm = new()
		{
			Action = ActionType.AUDIO
		};
		string result = alarm.ToString();

		Assert.Contains("ACTION:AUDIO", result);
	}

	[Fact]
	public void ToString_ActionEmail_IncludesCorrectAction()
	{
		VAlarm alarm = new()
		{
			Action = ActionType.EMAIL
		};
		string result = alarm.ToString();

		Assert.Contains("ACTION:EMAIL", result);
	}

	[Fact]
	public void ToString_CustomDescription_IncludesDescription()
	{
		string description = "Meeting starts soon!";
		VAlarm alarm = new()
		{
			Description = description
		};
		string result = alarm.ToString();

		Assert.Contains($"DESCRIPTION:{description}", result);
	}

	[Fact]
	public void ToString_FullCustomAlarm_ReturnsExpectedFormat()
	{
		string expected = """
			BEGIN:VALARM
			TRIGGER:-PT0D1H30M
			ACTION:EMAIL
			DESCRIPTION:Meeting reminder
			END:VALARM

			""";

		VAlarm alarm = new(
			TimeSpan.FromMinutes(-90),
			ActionType.EMAIL,
			"Meeting reminder"
		);
		string actual = alarm.ToString();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ToString_EndsWithNewline()
	{
		VAlarm alarm = new();
		string result = alarm.ToString();

		Assert.EndsWith(Environment.NewLine, result);
	}

	[Fact]
	public void ToString_ContainsTriggerLine()
	{
		VAlarm alarm = new();
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:", result);
	}

	[Fact]
	public void ToString_ContainsActionLine()
	{
		VAlarm alarm = new();
		string result = alarm.ToString();

		Assert.Contains("ACTION:", result);
	}

	[Fact]
	public void ToString_ContainsDescriptionLine()
	{
		VAlarm alarm = new();
		string result = alarm.ToString();

		Assert.Contains("DESCRIPTION:", result);
	}

	[Fact]
	public void ToString_TriggerHasNegativePrefix()
	{
		VAlarm alarm = new(TimeSpan.FromMinutes(-15));
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:-P", result);
	}

	[Theory]
	[InlineData(-15, "TRIGGER:-PT0D0H15M")]
	[InlineData(-30, "TRIGGER:-PT0D0H30M")]
	[InlineData(-60, "TRIGGER:-PT0D1H0M")]
	[InlineData(-1440, "TRIGGER:-PT1D0H0M")]
	public void ToString_VariousTriggerMinutes_FormatsCorrectly(int minutes, string expected)
	{
		VAlarm alarm = new(TimeSpan.FromMinutes(minutes));
		string result = alarm.ToString();

		Assert.Contains(expected, result);
	}

	[Fact]
	public void ToString_ZeroTrigger_FormatsCorrectly()
	{
		VAlarm alarm = new(TimeSpan.Zero);
		string result = alarm.ToString();

		Assert.Contains("TRIGGER:-PT0D0H0M", result);
	}

	[Fact]
	public void ToString_EmptyDescription_IncludesEmptyDescriptionLine()
	{
		VAlarm alarm = new()
		{
			Description = ""
		};
		string result = alarm.ToString();

		Assert.Contains("DESCRIPTION:", result);
		Assert.Contains("DESCRIPTION:\r\n", result);
	}

	[Fact]
	public void ToString_MultilineDescription_IncludesFullDescription()
	{
		string description = "Line 1\r\nLine 2";
		VAlarm alarm = new()
		{
			Description = description
		};
		string result = alarm.ToString();

		Assert.Contains($"DESCRIPTION:{description}", result);
	}

	[Theory]
	[InlineData(1, 0, 0, "TRIGGER:-PT1D0H0M")]
	[InlineData(0, 1, 0, "TRIGGER:-PT0D1H0M")]
	[InlineData(0, 0, 1, "TRIGGER:-PT0D0H1M")]
	[InlineData(1, 2, 30, "TRIGGER:-PT1D2H30M")]
	[InlineData(7, 0, 0, "TRIGGER:-PT7D0H0M")]
	public void ToString_VariousTriggerComponents_FormatsCorrectly(int days, int hours, int minutes, string expected)
	{
		TimeSpan trigger = new(days, hours, minutes, 0);
		VAlarm alarm = new(trigger);
		string result = alarm.ToString();

		Assert.Contains(expected, result);
	}
}
